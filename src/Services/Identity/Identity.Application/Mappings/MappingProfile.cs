using AutoMapper;
using Identity.Application.Features.Users.Commands.Edit;
using Identity.Application.Features.Users.Commands.Register;
using Identity.Domain.Entities;
using SessionGatewayService.Domain.Entities;

namespace Identity.Application.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<RegisterCommand, UserDbModel>();
		CreateMap<UserDbModel, UserVm>();
		CreateMap<UserDbModel, RegisterVm>();
		CreateMap<EditCommand, UserDbModel>();

	}
}
