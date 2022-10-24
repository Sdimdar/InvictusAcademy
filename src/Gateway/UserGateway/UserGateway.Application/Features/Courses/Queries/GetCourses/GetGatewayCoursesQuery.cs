using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Requests.Querries;
using ServicesContracts.Courses.Responses;

namespace UserGateway.Application.Features.Courses.Queries.GetCourses;

public class GetGatewayCoursesQuery : IRequest<Result<CoursesVm>>
{
    public string Email { get; set; }
    public CourseTypes Type { get; set; }
}
