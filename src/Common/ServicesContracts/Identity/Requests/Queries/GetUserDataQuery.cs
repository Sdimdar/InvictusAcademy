using Ardalis.Result;
using MediatR;
using ServicesContracts.Identity.Responses;

namespace ServicesContracts.Identity.Requests.Queries;

public class GetUserDataQuery : IRequest<Result<UserVm>>
{
    public string Email { get; set; }

    public GetUserDataQuery(string email)
    {
        Email = email;
    }
}
