using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Courses.Querries;

public class GetCoursesByIdListQuery : IRequest<Result<List<CoursesByIdVm>>>
{
    public List<int> CoursesId { get; set; }
}