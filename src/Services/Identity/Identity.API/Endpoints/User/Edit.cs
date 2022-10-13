using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using Identity.Application.Features.Users.Commands.Edit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Endpoints.User;

public class Edit : EndpointBaseAsync
    .WithRequest<EditCommand>
    .WithActionResult<DefaultResponceObject<string>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Edit(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new NullReferenceException(nameof(mediator));
        _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
    }

    [HttpPost("/User/Edit")]
    [SwaggerOperation(
        Summary = "Редактирование данных пользователя",
        Description = "Необходимо передать в теле запроса email пользователя",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<DefaultResponceObject<string>>> HandleAsync([FromBody] EditCommand request,
                                                                                        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponceObject<string>>(result));
    }
}