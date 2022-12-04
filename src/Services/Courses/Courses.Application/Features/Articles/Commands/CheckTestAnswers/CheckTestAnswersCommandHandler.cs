using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using Courses.Domain.Entities.CourseInfo.Tests;
using Courses.Domain.Entities.CourseResults;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Tests.Commands;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Features.Articles.Commands.CheckTestAnswers;

public class CheckTestAnswersCommandHandler : IRequestHandler<CheckTestAnswersCommand, Result<TestResultVm>>
{
    private readonly ICoursePurchasedRepository _coursePurchasedRepository;
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly IModuleInfoRepository _moduleInfoRepository;
    private readonly ICourseResultsInfoRepository _courseResultsInfoRepository;
    private readonly IValidator<CheckTestAnswersCommand> _validator;

    public CheckTestAnswersCommandHandler(ICoursePurchasedRepository coursePurchasedRepository,
                                          ICourseInfoRepository courseInfoRepository,
                                          IModuleInfoRepository moduleInfoRepository,
                                          ICourseResultsInfoRepository courseResultsInfoRepository,
                                          IValidator<CheckTestAnswersCommand> validator)
    {
        _coursePurchasedRepository = coursePurchasedRepository;
        _courseInfoRepository = courseInfoRepository;
        _moduleInfoRepository = moduleInfoRepository;
        _courseResultsInfoRepository = courseResultsInfoRepository;
        _validator = validator;
    }

    public async Task<Result<TestResultVm>> Handle(CheckTestAnswersCommand request, CancellationToken cancellationToken)
    {
        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return Result.Invalid(validateResult.AsErrors());
        }

        var coursePurchaseData = await _coursePurchasedRepository.GetFirstOrDefaultAsync(p => p.UserId == request.UserId && p.CourseId == request.CourseId);
        if (coursePurchaseData is null) return Result.Error($"Course is not purchased");

        var coursePurchaseResultData = await _courseResultsInfoRepository.GetAsync(coursePurchaseData.Id, cancellationToken);
        if (coursePurchaseResultData is null) throw new InvalidDataException($"Not found in mongo db info about result of " +
                                                                             $"course with ID: {request.CourseId} and user Id: {request.UserId}");
        if (coursePurchaseResultData.EndDate >= DateTime.Now) return Result.Error($"Course is not allowed now");

        var courseInfoData = await _courseInfoRepository.GetAsync(request.CourseId, cancellationToken);
        if (courseInfoData is null) return Result.Error($"Course info with ID {request.CourseId} is not exist");

        var modulesData = await _moduleInfoRepository.GetModulesByListOfIdAsync(courseInfoData.ModulesId, cancellationToken);

        var articleProgress = coursePurchaseResultData.ModuleProgresses.First(m => m.ModuleId == request.ModuleId)
                                                      .ArticlesProgresses.First(a => a.Order == request.ArticleOrder);

        if (!articleProgress.IsOpened)
        {
            return Result.Error("This test is not opened now, pass previous tests");
        }

        Test test;
        try
        {
            test = modulesData!.First(m => m.Id == request.ModuleId)
                               .Articles.First(a => a.Order == request.ArticleOrder)
                               .Test!;
        }
        catch (InvalidOperationException)
        {
            return Result.Error("Not found module or article or test");
        }
        TestResultVm result = CheckTestAnswers(test, request.Answers);
        var task = ChangeDbResultsStates(request, coursePurchaseData, coursePurchaseResultData, result, cancellationToken);

        return Result.Success(result);
    }

    private static TestResultVm CheckTestAnswers(Test test, List<CheckTestAnswersCommand.TestAnswer> answers)
    {
        TestResultVm result = new()
        {
            NeedAnswersCount = test.TestCompleteCount
        };

        int rightTestAnswersCount = 0;

        foreach (var answer in answers)
        {
            bool rightQuestionFlag = true;
            var testQuestion = test.TestQuestions.First(q => q.Id == answer.QuestionId);

            foreach (var rightAnswer in testQuestion.Answers)
            {
                if (rightAnswer.IsCorrect)
                {
                    if (answer.ChoosedAnswers.Any(a => a == rightAnswer.Id))
                    {
                        answer.ChoosedAnswers.Remove(rightAnswer.Id);
                    }
                    else
                    {
                        rightQuestionFlag = false;
                        break;
                    }
                }
            }
            if (rightQuestionFlag)
            {
                rightTestAnswersCount++;
            }
        }

        result.RightAnswersCount = rightTestAnswersCount;
        result.IsSucsess = rightTestAnswersCount >= test.TestCompleteCount;

        return result;
    }

    private async Task ChangeDbResultsStates(CheckTestAnswersCommand request,
                                             CoursePurchasedDbModel coursePurchaseData,
                                             CourseResultInfoDbModel coursePurchaseResultData,
                                             TestResultVm result,
                                             CancellationToken cancellationToken)
    {
        var moduleProgress = coursePurchaseResultData.ModuleProgresses.First(m => m.ModuleId == request.ModuleId);

        var articleProgress = moduleProgress.ArticlesProgresses.First(a => a.Order == request.ArticleOrder);

        TestAttempt attempt = new()
        {
            AttemptDate = DateTime.Now,
            IsSuccess = result.IsSucsess,
            Score = result.RightAnswersCount / result.NeedAnswersCount
        };

        articleProgress.TestAttempts.Add(attempt);

        if (result.IsSucsess)
        {
            articleProgress.IsSuccess = true;
            articleProgress.EndDate = DateTime.Now;
            articleProgress.Score = attempt.Score;
            var nextArticleProgress = moduleProgress.ArticlesProgresses.FirstOrDefault(a => a.Order == request.ArticleOrder + 1);
            if (nextArticleProgress != null)
            {
                nextArticleProgress.IsOpened = true;
            }
            else if (CheckArticlesStatus(moduleProgress, out float moduleScore))
            {
                moduleProgress.IsSuccess = true;
                moduleProgress.EndDate = DateTime.Now;
                moduleProgress.Score = moduleScore;
                if (CheckModulesStatus(coursePurchaseResultData.ModuleProgresses, out float courseScore))
                {
                    coursePurchaseResultData.EndDate = DateTime.Now;
                    coursePurchaseResultData.Score = courseScore;
                    await SetCourseIsCompletedStateInPostgres(coursePurchaseData.CourseId);
                }
            }
        }

        var task = _courseResultsInfoRepository.UpdateAsync(coursePurchaseData.Id, coursePurchaseResultData, cancellationToken);
    }

    private static bool CheckArticlesStatus(ModuleProgress moduleProgress, out float moduleScore)
    {
        moduleScore = 0.0f;
        bool sucsessFlag = true;
        foreach (var articleProgress in moduleProgress.ArticlesProgresses)
        {
            if (!articleProgress.IsSuccess)
            {
                sucsessFlag = false;
                break;
            }
            moduleScore += articleProgress.Score;
        }
        moduleScore /= moduleProgress.ArticlesProgresses.Count;
        return sucsessFlag;
    }

    private static bool CheckModulesStatus(List<ModuleProgress> moduleProgresses, out float courseScore)
    {
        courseScore = 0.0f;
        bool sucsessFlag = true;
        foreach (var moduleProgress in moduleProgresses)
        {
            if (!moduleProgress.IsSuccess)
            {
                sucsessFlag = false;
                break;
            }
            courseScore += moduleProgress.Score;
        }
        courseScore /= moduleProgresses.Count;
        return sucsessFlag;
    }

    private async Task SetCourseIsCompletedStateInPostgres(int courseId)
    {
        var coursePurchased = await _coursePurchasedRepository.GetFirstOrDefaultAsync(c => c.CourseId == courseId);
        if (coursePurchased != null)
        {
            coursePurchased.IsCompleted = true;
            await _coursePurchasedRepository.UpdateAsync(coursePurchased);
            return;
        }
        throw new InvalidOperationException("Course purchased was not found");
    }
}
