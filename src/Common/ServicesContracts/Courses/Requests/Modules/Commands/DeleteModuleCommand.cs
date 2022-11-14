using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Courses.Requests.Modules.Commands;

public class DeleteModuleCommand : IRequest<Result>
{
    public int Id { get; set; }
}
