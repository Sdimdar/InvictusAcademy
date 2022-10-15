using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Request.Requests.Commands;

public class ManagerCommentCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public string ManagerComment { get; set; }
}