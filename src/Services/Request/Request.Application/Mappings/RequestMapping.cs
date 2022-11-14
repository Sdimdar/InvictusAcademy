using AutoMapper;
using Request.Domain.Entities;
using ServicesContracts.Request.Requests.Commands;
using ServicesContracts.Request.Requests.Querries;
using ServicesContracts.Request.Responses;

namespace Request.Application.Mappings;

public class RequestMapping : Profile
{
    public RequestMapping()
    {
        CreateMap<CreateRequestCommand, RequestDbModel>();
        CreateMap<GetAllRequestsQuery, GetAllRequestVm>();
    }

}