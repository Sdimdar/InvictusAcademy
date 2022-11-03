using Ardalis.Result;
using Courses.Domain.Entities.CourseInfo;
using MediatR;

namespace ServicesContracts.Courses.Requests.Modules.Commands;

public class CreateModuleCommand : IRequest<Result<ModuleInfoDbModel>>
{
    public string Title { get; set; }
    public string ShortDescription { get; set; }
}
