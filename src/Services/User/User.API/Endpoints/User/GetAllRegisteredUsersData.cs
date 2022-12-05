using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Requests.Queries;
using ServicesContracts.Identity.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace User.API.Endpoints.User;

public class GetAllRegisteredUsersData : EndpointBaseAsync
    .WithRequest<GetUsersDataQuery>
    .WithActionResult<DefaultResponseObject<UsersVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllRegisteredUsersData> _logger;

    public GetAllRegisteredUsersData(IMediator mediator, IMapper mapper, ILogger<GetAllRegisteredUsersData> logger)
    {
        _mediator = mediator ?? throw new NullReferenceException(nameof(mediator));
        _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
        _logger = logger;
    }

    [HttpGet("/User/GetAllRegisteredUsersData")]
    [SwaggerOperation(
        Summary = "Получение данных пользователей",
        Description = "Для пагинации требуется вести в строку номер страницы, строка фильтра может быть пустой",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<UsersVm>>> HandleAsync([FromQuery] GetUsersDataQuery query,
                                                                                             CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(_mapper.Map<DefaultResponseObject<UsersVm>>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException?.Message);
            throw;
        }
    }
}