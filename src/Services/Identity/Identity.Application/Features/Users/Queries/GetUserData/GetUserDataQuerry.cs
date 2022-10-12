using Ardalis.Result;
using MediatR;
using SessionGatewayService.Domain.Entities;

namespace Identity.Application.Features.Users.Queries.GetUserData;

public class GetUserDataQuerry : IRequest<Result<UserVm>>
{
    public string Email { get; set; }

    public GetUserDataQuerry(string email)
    {
        Email = email;
    }
}
