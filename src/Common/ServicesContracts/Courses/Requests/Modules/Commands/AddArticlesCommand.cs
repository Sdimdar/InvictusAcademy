using Ardalis.Result;
using Courses.Domain.Entities.CourseInfo;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Modules.Commands;

public class AddArticlesCommand : IRequest<Result<ModuleInfoVm>>
{
    public int ModuleId { get; set; }
    public List<Articles> Articles { get; set; }
}
