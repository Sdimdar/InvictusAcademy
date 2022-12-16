using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Payments.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Payment.API.Endpoints.Payments;

public class Reject : EndpointBaseAsync
    .WithRequest<RejectPaymentCommand>
    .WithActionResult<DefaultResponseObject<bool>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Reject(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Payments/Reject")]
    [SwaggerOperation(
        Summary = "Отклонение заявки на платеж",
        Description = "Необходимо передать в теле запроса Id платежа и Email админа отклонившего заявку на платёж." +
                      "А также строку с объяснением почему платёж был отклонён.",
        Tags = new[] { "Payments" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<bool>>> HandleAsync([FromBody] RejectPaymentCommand request,
                                                                                      CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<bool>>(result));
    }
}