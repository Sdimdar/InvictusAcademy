using Ardalis.Result;
using MediatR;

namespace Identity.Application.Features.Users.Queries.Login;

public class LoginQuerry : IRequest<Result<string>>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }

    public LoginQuerry(string email, string password, bool rememberMe)
    {
        Email = email;
        Password = password;
        RememberMe = rememberMe;
    }
}
