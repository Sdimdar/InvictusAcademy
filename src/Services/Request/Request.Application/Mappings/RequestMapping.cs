using AutoMapper;
using Request.Application.Features.Requests.Commands.CreateRequest;
using Request.Application.Features.Requests.Queries.GetAllRequest;
using Request.Domain.Entities;
using ServicesContracts.Request.Requests.Commands;
using ServicesContracts.Request.Requests.Querries;

namespace Request.Application.Mappings;

public class RequestMapping : Profile
{
    public RequestMapping()
    {
        CreateMap<CreateRequestCommand, RequestDbModel>();
        CreateMap<GetAllRequestCommand, GetAllRequestVm>();
    }

}