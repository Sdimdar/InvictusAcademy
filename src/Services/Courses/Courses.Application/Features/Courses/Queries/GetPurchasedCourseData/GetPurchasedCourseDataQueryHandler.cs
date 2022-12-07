using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Features.Courses.Queries.GetPurchasedCourseData;

public class GetPurchasedCourseDataQueryHandler : IRequestHandler<GetPurchasedCourseDataQuery, Result<PurchasedCourseInfoVm>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICoursePurchasedRepository _coursePurchasedRepository;
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly IModuleInfoRepository _moduleInfoRepository;
    private readonly ICourseResultsInfoRepository _courseResultsInfoRepository;
    private readonly IValidator<GetPurchasedCourseDataQuery> _validator;

    public GetPurchasedCourseDataQueryHandler(IValidator<GetPurchasedCourseDataQuery> validator,
                                              ICourseRepository courseRepository,
                                              ICoursePurchasedRepository coursePurchasedRepository,
                                              ICourseInfoRepository courseInfoRepository,
                                              IModuleInfoRepository moduleInfoRepository,
                                              ICourseResultsInfoRepository courseResultsInfoRepository)
    {
        _validator = validator;
        _courseRepository = courseRepository;
        _coursePurchasedRepository = coursePurchasedRepository;
        _courseInfoRepository = courseInfoRepository;
        _moduleInfoRepository = moduleInfoRepository;
        _courseResultsInfoRepository = courseResultsInfoRepository;
    }

    public async Task<Result<PurchasedCourseInfoVm>> Handle(GetPurchasedCourseDataQuery request,
                                                            CancellationToken cancellationToken)
    {
        var validationData = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationData.IsValid)
        {
            return Result.Invalid(validationData.AsErrors());
        }

        var courseData = await _courseRepository.GetByIdAsync(request.CourseId);
        if (courseData is null) return Result.Error($"Course with ID {request.CourseId} is not exist");

        var coursePurchaseData = await _coursePurchasedRepository.GetFirstOrDefaultAsync(p => p.UserId == request.UserId && p.CourseId == request.CourseId);
        if (coursePurchaseData is null) return Result.Error($"Course is not purchased");

        var coursePurchaseResultData = await _courseResultsInfoRepository.GetAsync(coursePurchaseData.Id, cancellationToken);
        if (coursePurchaseResultData is null) throw new InvalidDataException($"Not found in mongo db info about result of " +
                                                                             $"course with ID: {request.CourseId} and user Id: {request.UserId}");
        if (coursePurchaseResultData.EndDate >= DateTime.Now) return Result.Error($"Course is not allowed now");

        var courseInfoData = await _courseInfoRepository.GetAsync(request.CourseId, cancellationToken);
        if (courseInfoData is null) return Result.Error($"Course info with ID {request.CourseId} is not exist");

        var modulesData = await _moduleInfoRepository.GetModulesByListOfIdAsync(courseInfoData.ModulesId, cancellationToken);

        List<PurchasedModuleInfoVm> shortModules = new();
        foreach (var item in modulesData!)
        {
            shortModules.Add(new PurchasedModuleInfoVm()
            {
                Id = item.Id,
                ShortDescription = item.ShortDescription,
                Title = item.Title,
                ArticlesCount = item.Articles?.Count?? 0,
                CompletedArticlesCount = coursePurchaseResultData.ModuleProgresses.First(m => m.ModuleId == item.Id)
                                                              .ArticlesProgresses?.Where(a => a.IsSuccess)
                                                                                  .ToList().Count?? 0,
                IsCompleted = coursePurchaseResultData.ModuleProgresses.First(o => o.ModuleId == item.Id).IsSuccess
            });
        }

        int completedModulesCount = 0;
        ShortModuleInfoVm? nextLearningModule = null;
        ShortArticleInfoVm? nextLearningArticle = null;
        foreach (var module in coursePurchaseResultData.ModuleProgresses)
        {
            if (module.EndDate is not null)
            {
                completedModulesCount++;
            }
            else
            {
                ModuleInfoDbModel moduleInfo = modulesData.First(m => m.Id == module.ModuleId);
                nextLearningModule ??= new ShortModuleInfoVm()
                {
                    Id = moduleInfo.Id,
                    Title = moduleInfo.Title,
                    ShortDescription = moduleInfo.ShortDescription
                };
                foreach (var article in module.ArticlesProgresses)
                {
                    if (article.IsOpened)
                    {
                        Article? articleInfo = moduleInfo.Articles?.First(a => a.Order == article.Order);
                        if (articleInfo is null) break;

                        nextLearningArticle ??= new ShortArticleInfoVm()
                        {
                            Order = article.Order,
                            Title = articleInfo.Title,
                            IsCompleted = article.IsSuccess,
                            IsOpened = article.IsOpened,
                        };
                        break;
                    }
                }
            }
        }

        PurchasedCourseInfoVm result = new()
        {
            Id = request.CourseId,
            Description = courseData.Description,
            Name = courseData.Name,
            Modules = shortModules,
            CompletedModulesCount = completedModulesCount,
            NextLearingModule = nextLearningModule,
            NextLearningArticle = nextLearningArticle
        };

        return Result.Success(result);
    }
}
