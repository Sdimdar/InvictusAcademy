using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo.Tests;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.Courses.Requests.Tests.Queries;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Features.Articles.Queries.GetPurchasedTest;

public class GetPurchasedTestQueryHandler : IRequestHandler<GetPurchasedTestQuery, Result<List<PurchasedTestVm>>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICoursePurchasedRepository _coursePurchasedRepository;
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly IModuleInfoRepository _moduleInfoRepository;
    private readonly ICourseResultsInfoRepository _courseResultsInfoRepository;
    private readonly IValidator<GetPurchasedTestQuery> _validaotor;
    private readonly ILogger<GetPurchasedTestQueryHandler> _logger;

    public GetPurchasedTestQueryHandler(ICourseRepository courseRepository,
                                          ICoursePurchasedRepository coursePurchasedRepository,
                                          ICourseInfoRepository courseInfoRepository,
                                          IModuleInfoRepository moduleInfoRepository,
                                          ICourseResultsInfoRepository courseResultsInfoRepository,
                                          IValidator<GetPurchasedTestQuery> validaotor, ILogger<GetPurchasedTestQueryHandler> logger)
    {
        _courseRepository = courseRepository;
        _coursePurchasedRepository = coursePurchasedRepository;
        _courseInfoRepository = courseInfoRepository;
        _moduleInfoRepository = moduleInfoRepository;
        _courseResultsInfoRepository = courseResultsInfoRepository;
        _validaotor = validaotor;
        _logger = logger;
    }

    public async Task<Result<List<PurchasedTestVm>>> Handle(GetPurchasedTestQuery request,
                                                      CancellationToken cancellationToken)
    {
        var validateResult = await _validaotor.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return Result.Invalid(validateResult.AsErrors());
        }

        var courseData = await _courseRepository.GetByIdAsync(request.CourseId);
        if (courseData is null)
        {
            _logger.LogWarning($"{BussinesErrors.DataIsNotExist.ToString()}: Course with ID {request.CourseId} is not exist");
            return Result.Error($"{BussinesErrors.DataIsNotExist.ToString()}: Course with ID {request.CourseId} is not exist");
        }

        var coursePurchaseData = await _coursePurchasedRepository.GetFirstOrDefaultAsync(p => p.UserId == request.UserId && p.CourseId == request.CourseId);
        if (coursePurchaseData is null)
        {
            _logger.LogWarning($"{BussinesErrors.CourseIsNotPurchased.ToString()}: Course is not purchased");
            return Result.Error($"{BussinesErrors.CourseIsNotPurchased.ToString()}: Course is not purchased");
        }

        var coursePurchaseResultData = await _courseResultsInfoRepository.GetAsync(coursePurchaseData.Id, cancellationToken);
        if (coursePurchaseResultData is null){
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Not found in mongo db info about result of " +
                               $"course with ID: {request.CourseId} and user Id: {request.UserId}");
            throw new InvalidDataException($"{BussinesErrors.NotFound.ToString()}: Not found in mongo db info about result of " +
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
            _logger.LogWarning($"{BussinesErrors.DataIsNotExist.ToString()}: Course info with ID {request.CourseId} is not exist");
            return Result.Error($"{BussinesErrors.DataIsNotExist.ToString()}: Course info with ID {request.CourseId} is not exist");
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
            return Result.Error("Not found module or article or test");
        }

        List<PurchasedTestVm> tests = GetTestForPassing(test);

        return Result.Success(tests);
    }

    private List<PurchasedTestVm> GetTestForPassing(Test test)
    {
        if (test.TestShowCount >= test.TestQuestions.Count)
        {
            return CreateListOfTestVmFromListOfDbTest(test.TestQuestions);
        }
        return CreateListOfTestVmFromListOfDbTest(SelectRandomTests(test.TestQuestions, test.TestShowCount));
    }

    private List<PurchasedTestVm> CreateListOfTestVmFromListOfDbTest(List<TestQuestion> testQuestions)
    {
        List<PurchasedTestVm> tests = new();
        foreach (var testQuestion in testQuestions)
        {
            List<PurchasedTestAnswerVm> answers = new();
            foreach (var answer in testQuestion.Answers)
            {
                answers.Add(new PurchasedTestAnswerVm()
                {
                    Id = answer.Id,
                    TestAnswer = answer.Text
                });

            }
            tests.Add(new PurchasedTestVm()
            {
                TestId = testQuestion.Id,
                QuestionType = testQuestion.QuestionType,
                TestQuestion = testQuestion.Question,
                TestAnswers = answers
            });
        }
        return tests;

    }

    private static List<TestQuestion> SelectRandomTests(List<TestQuestion> list, int testShowCount)
    {
        var result = list.Take(testShowCount).ToList();
        var random = new Random();
        for (int i = testShowCount; i < list.Count; i++)
        {
            int index = random.Next(i);
            if (index < testShowCount) result[index] = list[i];
        }
        return result;
    }

}
