using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.CloudStorage.Requests.Queries;
using ServicesContracts.CloudStorage.Requests.Querries;
using Swashbuckle.AspNetCore.Annotations;

namespace CloudStorage.API.Endpoints;

public class GetFilesCount : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<DefaultResponseObject<int>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public GetFilesCount(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    [HttpGet("/CloudStorage/GetFilesCount")]
    [SwaggerOperation(
        Summary = "Запрос на выгрузку всех файлов",
        Description = "Могут запрашивать только пользователи с ролью админ",
        Tags = new[] { "CloudStorage" })
    ]
    public async override Task<ActionResult<DefaultResponseObject<int>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await _mediator.Send(new GetFilesCountQuery(), cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<int>>(response));
    }
}