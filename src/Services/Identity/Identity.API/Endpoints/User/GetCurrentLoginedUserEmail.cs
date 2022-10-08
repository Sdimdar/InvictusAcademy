using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Identity.Application.Features.Users.Queries.GetCurrrentLoginedUserEmail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace Identity.API.Endpoints.User;

public class GetCurrentLoginedUserEmail : EndpointBaseSync
    .WithoutRequest
    .WithResult<ActionResult>
{

    [HttpGet("user/getlogineduserdata")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Получение данных о текущем залогиненном пользователе",
        Description = "Для получения данных о пользователе необходимо отправить пустой запрос",
        Tags = new[] { "User" })
    ]
    public override ActionResult Handle()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity == null) return Ok(Result.NotFound("The Jwt token is invalid"));
        try
        {
            string email = identity.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Email)!.Value;
            return Ok(Result.Success(new GetCurrentLoginedUserEmailVm() { Email = email }));
        }
        catch (Exception ex)
        {
            return Ok(Result.Error(ex.Message));
        }
    }
}
