using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Request.Requests.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace User.API.Endpoints.User;

public class ChangeBanStatus : EndpointBaseAsync
    .WithRequest<ToBanCommand>
    .WithActionResult<DefaultResponseObject<string>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ChangeBanStatus(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/User/SetBanStatus")]
    [SwaggerOperation(
        Summary = "Возможность банить пользователей",
        Description = "Необходимо передать id пользователя",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<string>>> HandleAsync(ToBanCommand request, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<string>>(response));
    }
}