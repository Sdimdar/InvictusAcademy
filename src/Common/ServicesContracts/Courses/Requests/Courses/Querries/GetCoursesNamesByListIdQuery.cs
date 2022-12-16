using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Courses.Querries;

public class GetCoursesNamesByListIdQuery : IRequest<Result<List<CoursesByIdVm>>>
{
    public List<int> ListId { get; set; }
}