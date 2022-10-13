using AutoMapper;
using Identity.Application.Features.Users.Commands.Edit;
using Identity.Application.Features.Users.Queries.GetUserData;
using Identity.Application.Features.Users.Queries.Login;
using Identity.Application.Features.Users.Commands.Register;
using Identity.Domain.Entities;
using Identity.Application.Features.Users.Queries.GetCurrrentLoginedUserEmail;
using SessionGatewayService.Domain.Entities;

namespace Identity.Application.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<RegisterCommand, UserDbModel>();
		CreateMap<UserDbModel, UserVm>();
		CreateMap<UserDbModel, LoginQuerryVm>();
        CreateMap<UserDbModel, RegisterCommandVm>();
        CreateMap<UserDbModel, GetCurrentLoginedUserEmailVm>();
        CreateMap<EditCommand, UserDbModel>();

	}
}
