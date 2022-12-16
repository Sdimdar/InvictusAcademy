using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.CloudStorage.Requests.Querries;
using ServicesContracts.CloudStorage.Responses;
using ServicesContracts.Identity.Requests.Queries;
using ServicesContracts.Identity.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace CloudStorage.API.Endpoints;

public class GetAllFiles : EndpointBaseAsync
    .WithRequest<GetAllFilesQuery>
    .WithActionResult<DefaultResponseObject<GetAllFilesVM>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllFiles> _logger;
    public GetAllFiles(IMediator mediator, IMapper mapper, ILogger<GetAllFiles> logger)
    {
        _mediator = mediator ?? throw new NullReferenceException(nameof(mediator));
        _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
        _logger = logger;
    }
    [HttpGet("/CloudStorage/GetAllFiles")]
    [SwaggerOperation(
        Summary = "Получение данных пользователей",
        Description = "Для пагинации требуется вести в строку номер страницы, строка фильтра может быть пустой",
        Tags = new[] { "CloudStorage" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<GetAllFilesVM>>> HandleAsync([FromQuery] GetAllFilesQuery request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(_mapper.Map<DefaultResponseObject<GetAllFilesVM>>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException?.Message);
            throw;
        }
    }


}