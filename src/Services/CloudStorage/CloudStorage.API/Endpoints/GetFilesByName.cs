using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.CloudStorage.Requests.Queries;
using ServicesContracts.CloudStorage.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace CloudStorage.API.Endpoints;

public class GetFilesByName : EndpointBaseAsync
    .WithRequest<GetFilesByFilterStringQuery>
    .WithActionResult<DefaultResponseObject<GetFilesByNameVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<GetFilesByName> _logger;

    public GetFilesByName(IMediator mediator, IMapper mapper, ILogger<GetFilesByName> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }
    [HttpGet("/CloudStorage/GetFilesByName")]
    [SwaggerOperation(
        Summary = "Получение данных пользователей",
        Description = "Для пагинации требуется вести в строку номер страницы, строка фильтра может быть пустой",
        Tags = new[] { "CloudStorage" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<GetFilesByNameVm>>> HandleAsync([FromQuery]GetFilesByFilterStringQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(_mapper.Map<DefaultResponseObject<GetFilesByNameVm>>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException?.Message);
            throw;
        }
    }
}