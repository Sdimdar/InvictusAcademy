using Ardalis.Result;
using AutoMapper;
using Courses.Application.Contracts;
using MediatR;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Features.Courses.Queries.GetCoursesByIdList;

public class GetCoursesByIdListHandler : IRequestHandler<GetCoursesByIdListQuery, Result<List<CoursesByIdVm>>>
{
    private readonly IMapper _mapper;
    private readonly ICourseRepository _courseRepository;

    public GetCoursesByIdListHandler(IMapper mapper, ICourseRepository courseRepository)
    {
        _mapper = mapper;
        _courseRepository = courseRepository;
    }

    public async Task<Result<List<CoursesByIdVm>>> Handle(GetCoursesByIdListQuery request, CancellationToken cancellationToken)
    {
        
        if(!request.CoursesId.Any()) return Result.Error("Request list is empty");
        try
        {
            List<CoursesByIdVm> list = new();
            var test = await _courseRepository.GetCoursesByIdList(request.CoursesId);
            list = _mapper.Map<List<CoursesByIdVm>>(await _courseRepository.GetCoursesByIdList(request.CoursesId));
            if (!list.Any())
                return Result.Error("Response list is empty");
            return Result.Success(list);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}