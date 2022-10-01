using Ardalis.Result;
using MediatR;
using System.Security.Claims;

namespace Identity.Application.Features.Users.Queries.GetCurrrentLoginedUserEmail;

public class GetCurrentLoginedUserEmailQuerry : IRequest<Result<GetCurrentLoginedUserEmailVm>>
{
    public ClaimsPrincipal User { get; set; }
}
