using Ardalis.Result;
using MediatR;

namespace UserGateway.Application.Features.User.Querries.GetUserData;

public class GetUserDataQuerry : IRequest<Result<GetUserDataVm>>
{
    public string Email { get; set; }
}
