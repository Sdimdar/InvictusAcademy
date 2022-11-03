using Ardalis.Result;
using CommonStructures;
using MediatR;

namespace ServicesContracts.Courses.Requests.Courses.Querries;

public class GetCourseModulesIdQuerry : IRequest<Result<UnicueList<int>>>
{
    public int CourseId { get; set; }
}
