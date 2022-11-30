using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Payments.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Payment.API.Endpoints.Payments;

public class CancelPayment:EndpointBaseAsync
    .WithRequest<CancelPaymentCommand>
    .WithActionResult<DefaultResponseObject<bool>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CancelPayment(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    [HttpPost("/Payments/Cancel")]
    [SwaggerOperation(
        Summary = "Отклонение одобренного платежа",
        Description = "Необходимо передать в теле запроса Id платежа и Email админа отклонившего платёж." +
                      "А также строку с объяснением почему платёж был отклонён.",
        Tags = new[] { "Payments" })
    ]

    public override async Task<ActionResult<DefaultResponseObject<bool>>> HandleAsync([FromBody]CancelPaymentCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<bool>>(result));
    }
}