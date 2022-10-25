using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Requests.Querries;
using Swashbuckle.AspNetCore.Annotations;

namespace User.API.Endpoints.User;

public class GetUsersCount : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<DefaultResponseObject<int>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetUsersCount(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    [HttpGet("/User/GetAll")]
    [SwaggerOperation(
        Summary = "Запрос на выгрузку всех пользователей",
        Description = "Могут запрашивать только пользователи с ролью админ",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<int>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetAllUsersCountQuery(), cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<int>>(response));
    }
}