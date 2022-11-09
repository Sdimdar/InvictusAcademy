using Ardalis.Result;
using Courses.Domain.Entities.CourseInfo;
using MediatR;

namespace ServicesContracts.Courses.Requests.Modules.Queries;

public class GetFullByCourseIdQuery : IRequest<Result<List<ModuleInfoDbModel>?>>
{
    public int CourseId { get; set; }
    public int UserId { get; set; }
}