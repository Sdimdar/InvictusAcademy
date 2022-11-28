using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Courses.Application.Contracts;
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

        var coursePurchaseResultData = await _courseResultsInfoRepository.GetAsync(request.CourseId, cancellationToken);
        if (coursePurchaseResultData is null) throw new InvalidDataException($"Not found in mongo db info about result of " +
                                                                             $"course with ID: {request.CourseId} and user Id: {request.UserId}");
        if (coursePurchaseResultData.EndDate >= DateTime.Now) return Result.Error($"Course is not allowed now");

        var courseInfoData = await _courseInfoRepository.GetAsync(request.CourseId, cancellationToken);
        if (courseInfoData is null) return Result.Error($"Course info with ID {request.CourseId} is not exist");

        var modulesData = await _moduleInfoRepository.GetModulesByListOfIdAsync(courseInfoData.ModulesId, cancellationToken);

        List<ShortModuleInfoVm> shortModules = new();
        foreach (var item in modulesData!)
        {
            shortModules.Add(new ShortModuleInfoVm()
            {
                Id = item.Id,
                ShortDescription = item.ShortDescription,
                Title = item.Title,
            });
        }

        PurchasedCourseInfoVm result = new()
        {
            Id = request.CourseId,
            Description = courseData.Description,
            Name = courseData.Name,
            Modules = shortModules
        };

        return Result.Success(result);
    }
}
