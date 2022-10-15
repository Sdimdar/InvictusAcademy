using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Request.Requests.Commands;

public class ChangeCalledStatusCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
}