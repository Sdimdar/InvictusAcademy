using AutoMapper;
using Identity.Domain.Entities;
using SessionGatewayService.Domain.ServicesContracts.Identity.Requests.Commands;
using SessionGatewayService.Domain.ServicesContracts.Identity.Responses;

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
