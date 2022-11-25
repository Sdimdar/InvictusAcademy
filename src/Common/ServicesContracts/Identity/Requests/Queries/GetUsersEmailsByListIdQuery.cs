using Ardalis.Result;
using MediatR;
using ServicesContracts.Identity.Responses;

namespace ServicesContracts.Identity.Requests.Queries;

public class GetUsersEmailsByListIdQuery : IRequest<Result<List<UsersEmailsByListIdVm>>>
{
    public List<int> ListId { get; set; }
}