using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Identity.Application.Features.Users.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Endpoints.User
{
    public class Login : EndpointBaseAsync
        .WithRequest<LoginQuerry>
        .WithResult<Result<string>>
    {
        private readonly IMediator _mediator;

        public Login(IMediator mediator)
        {
            _mediator = mediator ?? throw new NullReferenceException(nameof(mediator));
        }

        [HttpPost("user/login")]
        [TranslateResultToActionResult]
        public override async Task<Result<string>> HandleAsync([FromBody] LoginQuerry request, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(request, cancellationToken);
        }
    }
}
