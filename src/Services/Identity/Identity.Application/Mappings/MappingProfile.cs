using AutoMapper;
using Identity.Domain.Entities;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;

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
