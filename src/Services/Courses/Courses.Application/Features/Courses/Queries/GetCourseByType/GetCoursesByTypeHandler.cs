using Ardalis.Result;
using AutoMapper;
using Courses.Application.Contracts;
using MediatR;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Features.Courses.Queries.GetCourseByType;

public class GetCoursesByTypeHandler : IRequestHandler<GetCoursesQuery, Result<CoursesVm>>
{

    private readonly IMapper _mapper;
    private readonly ICourseRepository _courseRepository;

    public GetCoursesByTypeHandler(IMapper mapper, ICourseRepository courseRepository)
    {
        _mapper = mapper;
        _courseRepository = courseRepository;
    }

    public async Task<Result<CoursesVm>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        List<CourseVm> list = new();
        switch (request.Type)
        {
            case CourseTypes.New:
                list = _mapper.Map<List<CourseVm>>(await _courseRepository.GetAllActiveCourses());
                break;
            case CourseTypes.Wished:
                list = _mapper.Map<List<CourseVm>>(await _courseRepository.GetWishedCourses(request.UserId));
                break;
            case CourseTypes.Current:
                list = _mapper.Map<List<CourseVm>>(await _courseRepository.GetStartedCourses(request.UserId));
                foreach (var course in list)
                {
                    course.Purchased = true;
                }
                break;
            case CourseTypes.Completed:
                list = _mapper.Map<List<CourseVm>>(await _courseRepository.GetCompletedCourses(request.UserId));
                break;
        }


        if (!list.Any())
            return Result.Error("Request list is empty");
        if (list == null)
            return Result.NotFound();

        var result = new CoursesVm
        {
            Courses = list
        };
        return Result.Success(result);
    }
}