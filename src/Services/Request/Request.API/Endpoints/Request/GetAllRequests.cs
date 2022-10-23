using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Request.Requests.Querries;
using ServicesContracts.Request.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Request.API.Endpoints.Request;

public class GetAllRequests : EndpointBaseAsync
    .WithRequest<GetAllRequestCommand>
    .WithActionResult<DefaultResponseObject<GetAllRequestVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetAllRequests(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Request/GetAll")]
    [SwaggerOperation(
        Summary = "Запрос на выгрузку всех заявок",
        Description = "Могут запрашивать только пользователи с ролью админ",
        Tags = new[] { "Request" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<GetAllRequestVm>>> HandleAsync([FromQuery] GetAllRequestCommand request,
                                                                                                 CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<GetAllRequestVm>>(response));
    }
}