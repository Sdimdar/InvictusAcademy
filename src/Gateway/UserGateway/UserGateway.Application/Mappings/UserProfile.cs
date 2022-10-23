using AutoMapper;
using ServicesContracts.Identity.Responses;
using UserGateway.Application.Features.User.Querries.GetUserData;

namespace UserGateway.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserVm, GetUserDataVm>();
    }
}
