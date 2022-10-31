using Ardalis.Result;
using Courses.Domain.Entities.CourseInfo;
using MediatR;

namespace ServicesContracts.Courses.Requests.Commands;

public class InsertModuleCommand : IRequest<Result<CourseInfoDbModel>>
{
    public int CourseId { get; set; }
    public int ModuleId { get; set; }
    public int Index { get; set; }
}
