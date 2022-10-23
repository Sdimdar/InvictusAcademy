using AutoMapper;
using Identity.Domain.Entities;
using Request.Domain.Entities;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;
using ServicesContracts.Request.Requests.Querries;
using ServicesContracts.Request.Requests.Commands;
using ServicesContracts.Request.Responses;
using SessionGatewayService.Application.Features.User.Querries.GetUserData;

namespace Identity.Application.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<RegisterCommand, UserDbModel>()
			.ForMember(x => x.Email, 
				o => 
					o.MapFrom(p => p.Email));
		CreateMap<UserDbModel, UserVm>();
		CreateMap<UserDbModel, GetUserDataVm>();
        CreateMap<UserDbModel, RegisterVm>();
        CreateMap<UserDbModel, GetUserDataQuerry>();
        
        //REQUESTS
        CreateMap<CreateRequestCommand, RequestDbModel>();
        CreateMap<GetAllRequestCommand, GetAllRequestVm>();

	}
}
