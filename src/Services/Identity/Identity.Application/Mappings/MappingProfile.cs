using AutoMapper;
using Identity.Application.Features.Users.Queries.GetUserData;
using Identity.Application.Features.Users.Queries.Login;
using Identity.Application.Features.Users.Commands.Register;
using Identity.Domain.Entities;
using Identity.Application.Features.Users.Queries.GetCurrrentLoginedUserEmail;

namespace Identity.Application.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<RegisterCommand, User>()
			.ForMember(x => x.UserName, o => o.MapFrom(p => p.Email));
		CreateMap<User, UserDataVm>();
		CreateMap<User, LoginQuerryVm>();
        CreateMap<User, RegisterCommandVm>();
        CreateMap<User, GetCurrentLoginedUserEmailVm>();
    }
}
