using Ardalis.Result;
using MediatR;

namespace SessionGatewayService.Application.Features.User.Querries.GetUserData;

public class GetUserDataQuerry : IRequest<Result<GetUserDataVm>>
{
    public string Email { get; set; }
}
