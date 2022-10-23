using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Request.Requests.Querries;
using Swashbuckle.AspNetCore.Annotations;

namespace Request.API.Endpoints.Request;

public class GetRequestsCount : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<DefaultResponseObject<int>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetRequestsCount(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("Request/Count")]
    [SwaggerOperation(
        Summary = "Запрос на получение количества заявок",
        Description = "Могут запрашивать только пользователи с ролью админ",
        Tags = new[] { "Request" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<int>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetRequestsCountQuerry(), cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<int>>(response));
    }
}
