using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Modules.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Module;

public class GetModulesCount : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<DefaultResponseObject<int>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetModulesCount(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    [HttpGet("/Module/GetModulesCount")]
    [SwaggerOperation(
        Summary = "Запрос на выгрузку всех модулей",
        Description = "Могут запрашивать только пользователи с ролью админ",
        Tags = new[] { "Module" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<int>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetModulesCountQuery(), cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<int>>(response));
    }
}