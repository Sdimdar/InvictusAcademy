using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Identity.Application.Features.Users.Queries.GetUserData;
using Identity.Application.Features.Users.Queries.GetUsersData;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Endpoints.User;

public class GetUsersData : EndpointBaseAsync
    .WithRequest<int>
    .WithResult<Result<UsersDataVm>>
{
    private readonly IMediator _mediator;

    public GetUsersData(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/user/getUsersData")]
    [TranslateResultToActionResult]
    [SwaggerOperation(
        Summary = "Получение данных пользователей",
        Description = "Для пагинации требуется вести в строку номер страницы",
        Tags = new[] { "User" })
    ]
    public override async Task<Result<UsersDataVm>> HandleAsync(int page, CancellationToken cancellationToken = default)
    {
        GetUsersDataQuerry command = new(page);
        return await _mediator.Send(command, cancellationToken);
    }
}