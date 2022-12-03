using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Courses.Querries;

public class GetCourseByIdQuery : IRequest<Result<CourseByIdVm>>
{
    public int Id { get; set; }
}
