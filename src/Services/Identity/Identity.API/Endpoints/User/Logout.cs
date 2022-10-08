using Ardalis.ApiEndpoints;
using Identity.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Endpoints.User;

public class Logout : EndpointBaseSync
    .WithoutRequest
    .WithoutResult
{
    [HttpPost("/user/logout")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Деавторизация пользователя",
        Description = "Только для авторизованных пользователей",
        Tags = new[] { "User" })
    ]
    public override void Handle()
    {
        HttpContext.Response.Cookies.RemoveJwtToken();
    }
}
