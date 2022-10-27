using Ardalis.Result;
using AutoMapper;
using Courses.Application.Contracts;
using MediatR;
using ServicesContracts.Courses.Requests.Querries;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Features.Courses.Queries.GetCourseByType;

public class GetCoursesByTypeHandler:IRequestHandler<GetCoursesQuery,Result<List<CourseVm>>>
{
    
    private readonly IMapper _mapper;
    private readonly ICourseRepository _courseRepository;
    private readonly ICoursePurchasedRepository _purchasedRepository;
    private readonly ICourseWishedRepository _wishedRepository;

    public GetCoursesByTypeHandler(IMapper mapper, ICourseRepository courseRepository, ICoursePurchasedRepository purchasedRepository, ICourseWishedRepository wishedRepository)
    {
        _mapper = mapper;
        _courseRepository = courseRepository;
        _purchasedRepository = purchasedRepository;
        _wishedRepository = wishedRepository;
    }

    public async Task<Result<List<CourseVm>>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        List<CourseVm> list = new();
        switch (request.Type)
        {
            case CourseTypes.New:
                list = _mapper.Map<List<CourseVm>>(await _courseRepository.GetAllActiveCourses());
                break;
            case CourseTypes.Wished:
                list = _mapper.Map<List<CourseVm>>(await _wishedRepository.GetWishedCourses(request.UserId));
                break;
            case CourseTypes.Current:
                list = _mapper.Map<List<CourseVm>>(await _purchasedRepository.GetStartedCourses(request.UserId));
                break;
            case CourseTypes.Completed:
                list = _mapper.Map<List<CourseVm>>(await _purchasedRepository.GetCompletedCourses(request.UserId));
                break;
        }

        
        if (!list.Any())
            return Result.Error("Request list is empty");
        if (list == null)
            return Result.NotFound();
        return Result.Success(list);
    }
}