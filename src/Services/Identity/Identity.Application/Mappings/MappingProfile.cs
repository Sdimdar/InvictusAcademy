using AutoMapper;
using Identity.Application.Features.Users.Queries.Register;
using Identity.Domain.Entities;

namespace Identity.Application.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<RegisterQuerry, User>()
			.ForMember(x => x.UserName, o => o.MapFrom(p => p.Email));
	}
}
