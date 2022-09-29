using AutoMapper;
using Identity.Application.Features.Users.Commands.GetUserData;
using Identity.Application.Features.Users.Queries.Login;
using Identity.Application.Features.Users.Queries.Register;
using Identity.Domain.Entities;

namespace Identity.Application.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<RegisterCommand, User>()
			.ForMember(x => x.UserName, o => o.MapFrom(p => p.Email));
		CreateMap<User, UserDataVm>();
		CreateMap<User, LoginQuerryVm>();
	}
}
