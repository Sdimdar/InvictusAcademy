using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Courses.Querries;

public class GetCoursesBySearchStringCommand : IRequest<Result<CoursesVm>>
{
    public string SearchString { get; set; }
}