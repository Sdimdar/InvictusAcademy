using Ardalis.Result;
using Courses.Domain.Entities.CourseInfo;
using MediatR;

namespace ServicesContracts.Courses.Requests.Courses.Commands;

public class RemoveModuleCommand : IRequest<Result<CourseInfoDbModel>>
{
    public int CourseId { get; set; }
    public int ModuleId { get; set; }
}
