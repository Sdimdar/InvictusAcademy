using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Courses.Requests.Querries;

public class GetCourseModulesIdQuerry : IRequest<Result<List<int>>>
{
    public int CourseId { get; set; }
}
