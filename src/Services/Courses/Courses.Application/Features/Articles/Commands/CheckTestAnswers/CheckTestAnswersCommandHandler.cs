using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using Courses.Domain.Entities.CourseInfo.Tests;
using Courses.Domain.Entities.CourseResults;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<CheckTestAnswersCommandHandler> _logger;

    public CheckTestAnswersCommandHandler(ICoursePurchasedRepository coursePurchasedRepository,
                                          ICourseInfoRepository courseInfoRepository,
                                          IModuleInfoRepository moduleInfoRepository,
                                          ICourseResultsInfoRepository courseResultsInfoRepository,
                                          IValidator<CheckTestAnswersCommand> validator, ILogger<CheckTestAnswersCommandHandler> logger)
    {
        _coursePurchasedRepository = coursePurchasedRepository;
        _courseInfoRepository = courseInfoRepository;
        _moduleInfoRepository = moduleInfoRepository;
        _courseResultsInfoRepository = courseResultsInfoRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<TestResultVm>> Handle(CheckTestAnswersCommand request, CancellationToken cancellationToken)
    {
        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return Result.Invalid(validateResult.AsErrors());
        }

        var coursePurchaseData = await _coursePurchasedRepository.GetFirstOrDefaultAsync(p => p.UserId == request.UserId && p.CourseId == request.CourseId);
        if (coursePurchaseData is null)
        {
            _logger.LogWarning($"{BussinesErrors.CourseIsNotPurchased.ToString()}: Course is not purchased");
            return Result.Error($"{BussinesErrors.CourseIsNotPurchased.ToString()}: Course is not purchased");
        }

        var coursePurchaseResultData = await _courseResultsInfoRepository.GetAsync(coursePurchaseData.Id, cancellationToken);
        if (coursePurchaseResultData is null)
        {
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Not found in mongo db info about result of " +
                               $"course with ID: {request.CourseId} and user Id: {request.UserId}");
            throw new InvalidDataException($"Not found in mongo db info about result of " +
                                           $"course with ID: {request.CourseId} and user Id: {request.UserId}");
        }

        if (coursePurchaseResultData.EndDate <= DateTime.Now)
        {
            _logger.LogWarning($"{BussinesErrors.IsNotAllowed.ToString()}: Course is not allowed now");
            return Result.Error($"{BussinesErrors.IsNotAllowed.ToString()}: Course is not allowed now");
        } 

        var courseInfoData = await _courseInfoRepository.GetAsync(request.CourseId, cancellationToken);
        if (courseInfoData is null)
        {
            _logger.LogWarning($"{BussinesErrors.DataIsNotExist}: Course info with ID {request.CourseId} is not exist");
            return Result.Error($"{BussinesErrors.DataIsNotExist}: Course info with ID {request.CourseId} is not exist");
        }

        var modulesData = await _moduleInfoRepository.GetModulesByListOfIdAsync(courseInfoData.ModulesId, cancellationToken);

        var articleProgress = coursePurchaseResultData.ModuleProgresses.First(m => m.ModuleId == request.ModuleId)
                                                      .ArticlesProgresses.First(a => a.Order == request.ArticleOrder);

        if (!articleProgress.IsOpened)
        {
            _logger.LogWarning($"{BussinesErrors.IsNotOpened.ToString()}: This test is not opened now, pass previous tests");
            return Result.Error($"{BussinesErrors.IsNotOpened.ToString()}: This test is not opened now, pass previous tests");
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
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Not found module or article or test");
            return Result.Error($"{BussinesErrors.NotFound.ToString()}: Not found module or article or test");
        }
        TestResultVm result = CheckTestAnswers(test, request.Answers);
        await ChangeDbResultsStates(request, coursePurchaseData, coursePurchaseResultData, result, cancellationToken);

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
                    coursePurchaseResultData.Score = courseScore;
                    await SetCourseIsCompletedStateInPostgres(coursePurchaseData.CourseId);
                }
            }
        }

        await _courseResultsInfoRepository.UpdateAsync(coursePurchaseData.Id, coursePurchaseResultData, cancellationToken);
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
        _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Course purchased was not found");
        throw new InvalidOperationException($"{BussinesErrors.NotFound.ToString()}: Course purchased was not found");
    }
}
