using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Identity.Requests.Commands;

public class ToBanCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
}