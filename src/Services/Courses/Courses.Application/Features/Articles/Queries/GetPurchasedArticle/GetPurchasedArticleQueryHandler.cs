using Ardalis.Result;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using Courses.Domain.Entities.CourseResults;
using MediatR;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Features.Articles.Queries.GetPurchasedArticle;

public class GetPurchasedArticleQueryHandler : IRequestHandler<GetPurchasedArticleQuery, Result<PurchasedArticleInfoVm>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICoursePurchasedRepository _coursePurchasedRepository;
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly IModuleInfoRepository _moduleInfoRepository;
    private readonly ICourseResultsInfoRepository _courseResultsInfoRepository;

    public GetPurchasedArticleQueryHandler(ICourseRepository courseRepository,
                                           ICoursePurchasedRepository coursePurchasedRepository,
                                           ICourseInfoRepository courseInfoRepository,
                                           IModuleInfoRepository moduleInfoRepository,
                                           ICourseResultsInfoRepository courseResultsInfoRepository)
    {
        _courseRepository = courseRepository;
        _coursePurchasedRepository = coursePurchasedRepository;
        _courseInfoRepository = courseInfoRepository;
        _moduleInfoRepository = moduleInfoRepository;
        _courseResultsInfoRepository = courseResultsInfoRepository;
    }

    public async Task<Result<PurchasedArticleInfoVm>> Handle(GetPurchasedArticleQuery request,
                                                             CancellationToken cancellationToken)
    {
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

        ModuleProgress moduleProgress;
        ArticleProgress articleProgress;
        try
        {
            moduleProgress = coursePurchaseResultData.ModuleProgresses.First(o => o.ModuleId == request.ModuleId);
            articleProgress = moduleProgress.ArticlesProgresses.First(o => o.Order == request.ArticleOrder);
        }
        catch (InvalidOperationException ex)
        {
            return Result.Error("Not found module or article");
        }


        if (!articleProgress.IsOpened)
        {
            return Result.Error("Article is not aviliable now, pass the previous tests");
        }

        articleProgress.StartDate ??= DateTime.Now;
        moduleProgress.StartDate ??= DateTime.Now;

        var task = _courseResultsInfoRepository.UpdateAsync(coursePurchaseResultData.Id, coursePurchaseResultData, cancellationToken);

        ModuleInfoDbModel module = modulesData!.First(m => m.Id == request.ModuleId);
        Article article = module.Articles!.First(a => a.Order == request.ArticleOrder);

        List<ShortArticleInfoVm> shortArticleInfoVms = new();
        foreach (var item in module.Articles)
        {
            ArticleProgress progress = coursePurchaseResultData.ModuleProgresses.First(o => o.ModuleId == request.ModuleId)
                                                               .ArticlesProgresses.First(o => o.Order == item.Order);
            shortArticleInfoVms.Add(new ShortArticleInfoVm()
            {
                Order = item.Order,
                Title = item.Title,
                IsCompleted = progress.IsSuccess,
                IsOpened = progress.IsOpened
            });
        }

        PurchasedArticleInfoVm result = new()
        {
            Order = article.Order,
            Text = article.Text,
            Title = article.Title,
            VideoLink = article.VideoLink,
            IsCompleted = articleProgress.IsSuccess,
            IsLastArticleInModule = article.Order == module.Articles.Count,
            ModuleInfo = new ShortModuleInfoVm()
            {
                Id = request.ModuleId,
                Title = module.Title,
                ShortDescription = module.ShortDescription
            },
            Articles = shortArticleInfoVms
        };
        return Result.Success(result);
    }
}
