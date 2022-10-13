using Ardalis.Result;
using MediatR;
using SessionGatewayService.Domain.ServicesContracts.Identity.Responses;

namespace SessionGatewayService.Domain.ServicesContracts.Identity.Requests.Querries;

public class GetUserDataQuerry : IRequest<Result<UserVm>>
{
    public string Email { get; set; }

    public GetUserDataQuerry(string email)
    {
        Email = email;
    }
}
