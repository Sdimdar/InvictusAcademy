using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Identity.Application.Features.Users.Queries.GetCurrrentLoginedUserEmail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Endpoints.User;

public class GetCurrentLoginedUserEmail : EndpointBaseAsync
    .WithoutRequest
    .WithResult<Result<GetCurrentLoginedUserEmailVm>>
{
    private readonly IMediator _mediator;

    public GetCurrentLoginedUserEmail(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("user/getlogineduserdata")]
    [TranslateResultToActionResult]
    [Authorize]
    [SwaggerOperation(
        Summary = "Получение данных о текущем залогиненном пользователе",
        Description = "Для получения данных о пользователе необходимо отправить пустой запрос",
        Tags = new[] { "User" })
    ]
    public override async Task<Result<GetCurrentLoginedUserEmailVm>> HandleAsync(CancellationToken cancellationToken = default)
    {
        GetCurrentLoginedUserEmailQuerry querry = new() { User = User };
        return await _mediator.Send(querry, cancellationToken);
    }
}
