using AutoMapper;
using SessionGatewayService.Application.Features.User.Querries.GetUserData;
using SessionGatewayService.Domain.ServicesContracts.Identity.Responses;

namespace SessionGatewayService.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserVm, GetUserDataVm>();
    }
}
