using Ardalis.ApiEndpoints;
using Identity.Application.Features.Users.Commands.Edit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Endpoints.User;

public class Edit: EndpointBaseAsync
    .WithRequest<EditCommand>
    .WithResult<ActionResult>
{
    private readonly IMediator _mediator;
    
    public Edit(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/User/Edit")]
    [SwaggerOperation(
        Summary = "Редактирование данных пользователя",
        Description = "Необходимо передать в теле запроса email пользователя",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult> HandleAsync(EditCommand request, 
        CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}