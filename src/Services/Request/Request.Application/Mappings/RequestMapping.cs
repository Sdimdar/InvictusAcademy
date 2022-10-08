using AutoMapper;
using Request.Application.Features.Requests.Commands.CreateRequest;
using Request.Application.Features.Requests.Queries.GetAllRequest;

namespace Request.Application.Mappings;

public class RequestMapping:Profile
{
    public RequestMapping()
    {
        CreateMap<CreateRequestCommand, Domain.Entities.Request>();
        CreateMap<GetAllRequestCommand, GetAllRequestVm>();
    }
    
}