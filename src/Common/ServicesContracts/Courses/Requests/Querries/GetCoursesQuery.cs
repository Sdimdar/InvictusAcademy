using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Querries;

public class GetCoursesQuery : IRequest<Result<List<CourseVm>>>
{
    public int UserId { get; set; }
    public CourseTypes Type { get; set; }
}