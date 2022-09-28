using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Identity.Application.Features.Users.Queries.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Endpoints.User
{
    public class Register : EndpointBaseAsync
        .WithRequest<RegisterQuerry>
        .WithResult<Result<string>>
    {
        private readonly IMediator _mediator;

        public Register(IMediator mediator)
        {
            _mediator = mediator ?? throw new NullReferenceException(nameof(mediator));
        }

        [HttpPost("user/register")]
        [TranslateResultToActionResult]
        public override async Task<Result<string>> HandleAsync(RegisterQuerry request, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(request, cancellationToken);
        }
    }
}
