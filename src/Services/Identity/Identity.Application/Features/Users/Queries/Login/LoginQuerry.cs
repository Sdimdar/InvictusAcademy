using Ardalis.Result;
using MediatR;
using System.Security.Claims;

namespace Identity.Application.Features.Users.Queries.Login;

public class LoginQuerry : IRequest<(List<Claim>?, Result<LoginQuerryVm>)>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}
