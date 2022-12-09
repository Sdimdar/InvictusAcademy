using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;
using UserGateway.Application.Features.Payments.Commands.Add;

namespace UserGateway.API.Endpoints.Payments;

public class Add : EndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<DefaultResponseObject<bool>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Add(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("Payments/Add")]
    [SwaggerOperation(
        Summary = "Создание запроса на покупку курса",
        Description = "Необходимо передать в теле запроса ID курса",
        Tags = new[] { "Payment" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<bool>>> HandleAsync([FromQuery] int courseId,
                                                                                      CancellationToken cancellationToken)
    {
        var email = HttpContext.Session.GetData("user").Email;
        if (email is null)
        {
            throw new UnauthorizedAccessException("User is not authorized");
        }
        AddPaymentCommand query = new()
        {
            CourseId = courseId,
            UserEmail = email
        };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<bool>>(result));
    }
}