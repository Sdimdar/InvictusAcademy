using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Request.Requests.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Request.API.Endpoints.Request;

public class ManagerComment : EndpointBaseAsync
    .WithRequest<ManagerCommentCommand>
    .WithActionResult<DefaultResponseObject<string>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ManagerComment(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("Request/AddComment")]
    [SwaggerOperation(
        Summary = "Сохранение комментария для заявки",
        Description = "Необходимо передать id заявки и комментарий",
        Tags = new[] { "Request" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<string>>> HandleAsync(ManagerCommentCommand request,
                                                                                        CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request);
        return Ok(_mapper.Map<DefaultResponseObject<string>>(result));
    }
}