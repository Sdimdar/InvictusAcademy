using Ardalis.Result;
using AutoMapper;
using CommonStructures;
using Courses.Application.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Features.Courses.Queries.GetCourseByType;

public class GetCoursesByTypeHandler : IRequestHandler<GetCoursesQuery, Result<CoursesVm>>
{

    private readonly IMapper _mapper;
    private readonly ICourseRepository _courseRepository;
    private readonly ILogger<GetCoursesByTypeHandler> _logger;

    public GetCoursesByTypeHandler(IMapper mapper, ICourseRepository courseRepository, ILogger<GetCoursesByTypeHandler> logger)
    {
        _mapper = mapper;
        _courseRepository = courseRepository;
        _logger = logger;
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
            case CourseTypes.All:
                list = _mapper.Map<List<CourseVm>>(await _courseRepository.GetAllCourses());
                break;
        }


        if (!list.Any())
        {
            _logger.LogWarning($"{BussinesErrors.ListIsEmpty.ToString()}: Request list is empty");
            return Result.Error($"{BussinesErrors.ListIsEmpty.ToString()}: Request list is empty");
        }

        if (list == null)
        {
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Courses not found");
            return Result.NotFound($"{BussinesErrors.NotFound.ToString()}: Courses not found");
        }

        var result = new CoursesVm
        {
            Courses = list
        };
        return Result.Success(result);
    }
}