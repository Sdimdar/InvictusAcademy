using Ardalis.Result;
using Courses.Domain.Entities.CourseInfo.Tests;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Modules.Commands;

public class AddTestCommand : IRequest<Result<ModuleInfoVm>>
{
    public int ModuleId { get; set; }
    public int Order { get; set; }
    public Test Test { get; set; }
}