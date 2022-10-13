using AutoMapper;
using ServicesContracts.Identity.Responses;
using SessionGatewayService.Application.Features.User.Querries.GetUserData;

namespace SessionGatewayService.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserVm, GetUserDataVm>();
    }
}
