using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Identity.Application.Features.Users.Commands.GetUserData;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Endpoints.User;

public class GetUserData : EndpointBaseAsync
    .WithRequest<string>
    .WithResult<Result<UserDataVm>>
{
    private readonly IMediator _mediator;

    public GetUserData(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/user/getUserData")]
    [TranslateResultToActionResult]
    [SwaggerOperation(
        Summary = "Получение данных о пользователе",
        Description = "Для получения данных о пользователе необходимо передать его email через параметры в строке",
        Tags = new[] { "User" })
    ]
    public override async Task<Result<UserDataVm>> HandleAsync(string email, CancellationToken cancellationToken = default)
    {
        GetUserDataQuerry command = new(email);
        return await _mediator.Send(command, cancellationToken);
    }
}
