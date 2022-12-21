using System.Globalization;
using AutoMapper;
using Request.Domain.Entities;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;
using ServicesContracts.Request.Requests.Commands;
using ServicesContracts.Request.Requests.Querries;
using ServicesContracts.Request.Responses;
using User.Domain.Entities;
using UserGateway.Application.Features.User.Querries.GetUserData;

namespace User.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterCommand, UserDbModel>()
            .ForMember(x => x.Email,
                o =>
                    o.MapFrom(p => p.Email));
        CreateMap<UserDbModel, UserVm>().ForMember(x=>x.RegistrationDate, opt=>opt.MapFrom(e=>e.RegistrationDate.ToString("g", CultureInfo.GetCultureInfo("de-DE"))));
        CreateMap<UserDbModel, GetUserDataVm>();
        CreateMap<UserDbModel, RegisterVm>();
        CreateMap<UserDbModel, GetUserDataQuerry>();
        CreateMap<UserDbModel, UsersEmailsByListIdVm>();

        //REQUESTS
        CreateMap<CreateRequestCommand, RequestDbModel>();
        CreateMap<GetAllRequestsQuery, GetAllRequestVm>();

    }
}
