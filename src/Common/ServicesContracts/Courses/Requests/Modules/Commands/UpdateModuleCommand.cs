using Ardalis.Result;
using Courses.Domain.Entities.CourseInfo;
using MediatR;

namespace ServicesContracts.Courses.Requests.Modules.Commands;

public class UpdateModuleCommand : IRequest<Result<ModuleInfoDbModel>>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public List<Articles>? Articles { get; set; } = null;
}
