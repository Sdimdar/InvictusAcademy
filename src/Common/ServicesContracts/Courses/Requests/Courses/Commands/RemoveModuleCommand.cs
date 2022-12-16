using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Courses.Commands;

public class RemoveModuleCommand : IRequest<Result<CourseInfoVm>>
{
    public int CourseId { get; set; }
    public int ModuleId { get; set; }
}
