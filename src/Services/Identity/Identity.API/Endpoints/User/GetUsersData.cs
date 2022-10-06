using Ardalis.ApiEndpoints;
using Identity.Application.Features.Users.Queries.GetUsersData;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Endpoints.User;

public class GetUsersData : EndpointBaseAsync
    .WithRequest<GetUsersDataQuerry>
    .WithResult<ActionResult>
{
    private readonly IMediator _mediator;

    public GetUsersData(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/user/getUsersData")]
    [SwaggerOperation(
        Summary = "Получение данных пользователей",
        Description = "Для пагинации требуется вести в строку номер страницы, строка фильтра может быть пустой",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult> HandleAsync([FromQuery] GetUsersDataQuerry command, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }
}