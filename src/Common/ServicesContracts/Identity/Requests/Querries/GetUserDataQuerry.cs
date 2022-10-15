using Ardalis.Result;
using MediatR;
using ServicesContracts.Identity.Responses;

namespace ServicesContracts.Identity.Requests.Querries;

public class GetUserDataQuerry : IRequest<Result<UserVm>>
{
    public string Email { get; set; }

    public GetUserDataQuerry(string email)
    {
        Email = email;
    }
}
