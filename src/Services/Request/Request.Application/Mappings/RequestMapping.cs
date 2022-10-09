using AutoMapper;
using Request.Application.Features.Requests.Commands.CreateRequest;
using Request.Application.Features.Requests.Queries.GetAllRequest;
using Request.Domain.Entities;

namespace Request.Application.Mappings;

public class RequestMapping : Profile
{
    public RequestMapping()
    {
        CreateMap<CreateRequestCommand, RequestDbModel>();
        CreateMap<GetAllRequestCommand, GetAllRequestVm>();
    }

}