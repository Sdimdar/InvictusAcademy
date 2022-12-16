using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Courses.Querries;

public class GetStartedCoursesQuery : IRequest<Result<List<StartedCourseInfoVm>>>
{
    public List<UserIdCourseIdQuery> ListOfId { get; set; }
}