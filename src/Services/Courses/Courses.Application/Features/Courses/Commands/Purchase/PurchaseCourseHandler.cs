using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using Courses.Domain.Entities.CourseResults;
using Courses.Domain.Options;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServicesContracts.Courses.Requests.Courses.Commands;

namespace Courses.Application.Features.Courses.Commands.Purchase;

public class PurchaseCourseHandler : IRequestHandler<PurchaseCourseCommand, Result<bool>>
{
    private readonly IValidator<PurchaseCourseCommand> _validator;
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly IModuleInfoRepository _moduleInfoRepository;
    private readonly ICourseResultsInfoRepository _courseResultsInfoRepository;
    private readonly IOptions<CourseResultOptions> _courseOptions;
    private readonly ILogger<PurchaseCourseCommand> _logger;
    
    public PurchaseCourseHandler(IValidator<PurchaseCourseCommand> validator, 
                                 ICourseRepository courseRepository, 
                                 ICourseResultsInfoRepository courseResultsInfoRepository, 
                                 ICourseInfoRepository courseInfoRepository, 
                                 IModuleInfoRepository moduleInfoRepository, 
                                 IOptions<CourseResultOptions> courseOptions, 
                                 ILogger<PurchaseCourseCommand> logger)
    {
        _validator = validator;
        _courseRepository = courseRepository;
        _courseResultsInfoRepository = courseResultsInfoRepository;
        _courseInfoRepository = courseInfoRepository;
        _moduleInfoRepository = moduleInfoRepository;
        _courseOptions = courseOptions;
        _logger = logger;
    }
    
    public async Task<Result<bool>> Handle(PurchaseCourseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"A request to create a paid table came in: " +
                               $"UserId - {request.UserId}, CourseId - {request.CourseId} ");
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            _logger.LogInformation($"UserId - {request.UserId}, CourseId - {request.CourseId}. Is invalid");
            return Result.Invalid(validationResult.AsErrors());
        }

        var course = await _courseRepository.GetCourseById(request.CourseId);
        if (course is null)
        {
            _logger.LogInformation($"Course with Id: \'{request.CourseId}\' not founded");
            return Result.Error("Course is not found by ID");
        }

        if (await _courseRepository.CourseIsPaid(request.UserId, request.CourseId))
        {
            _logger.LogInformation($"Course with Id: \'{request.CourseId}\' is already paid by" +
                                   $"User with Id: \'{request.UserId}\'");
            return Result.Error("Course is already paid");
        }

        try
        {
            //TODO: Добавить добавление таблицы в Postgres

            var courseInfo = await _courseInfoRepository.GetAsync(request.CourseId, cancellationToken);
            if (courseInfo is null)
            {
                _logger.LogInformation($"Course with Id: \'{request.CourseId}\' is broken don't have info");
                return Result.Error("Course is broken don't have info");
            }
            
            CourseResultInfoDbModel entity = new()
            {   
                Id = request.CourseId,
                Score = 0.0f,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now + TimeSpan.FromDays(_courseOptions.Value.CourseDayDuration),
                ModuleProgresses = await CreateModulesProgressDataAsync(courseInfo, cancellationToken)
            };
        
            await _courseResultsInfoRepository.CreateAsync(entity, cancellationToken);
            _logger.LogInformation($"Course with Id: \'{request.CourseId}\' is successful paid for" +
                                   $"User with Id: \'{request.UserId}\'");
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    private async Task<List<ModuleProgress>> CreateModulesProgressDataAsync(CourseInfoDbModel courseInfo, 
                                                                           CancellationToken cancellationToken)
    {
        List<ModuleProgress> moduleProgresses = new();
        var courseModules = await _moduleInfoRepository.GetModulesByListOfIdAsync(courseInfo.ModulesId, cancellationToken);
        if (courseModules is null) return moduleProgresses;
        
        foreach (var moduleInfo in courseModules)
        {
            moduleProgresses.Add(new ModuleProgress()
            {
                Id = moduleInfo.Id,
                IsOpened = true,
                IsSuccess = false,
                Score = 0.0f,
                EndDate = null,
                StartDate = null,
                ArticlesProgresses = CreateArticlesProgressData(moduleInfo)
            });
        }
        
        return moduleProgresses;
    }

    private static List<ArticleProgress> CreateArticlesProgressData(ModuleInfoDbModel moduleInfo)
    {
        List<ArticleProgress> articlesProgresses = new();
        if (moduleInfo.Articles is null) return articlesProgresses;
        
        for (int i = 0; i < moduleInfo.Articles.Count; i++)
        {
            articlesProgresses.Add(new ArticleProgress()
            {
                Order = moduleInfo.Articles[i].Order,
                IsOpened = i == 0,
                IsSuccess = false,
                Score = 0.0f,
                EndDate = null,
                StartDate = null,
                TestAttempts = new List<TestAttempt>()
            });
        }

        return articlesProgresses;
    }
}