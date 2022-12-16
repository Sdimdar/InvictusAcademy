using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Payments.Models;
using ServicesContracts.Payments.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Payment.API.Endpoints.Payments;

public class GetById : EndpointBaseAsync
    .WithRequest<GetPaymentQuery>
    .WithActionResult<DefaultResponseObject<PaymentVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetById(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Payments/Get")]
    [SwaggerOperation(
        Summary = "Получение платежа",
        Description = "Необходимо передать в сроке запроса Id платежа",
        Tags = new[] { "Payments" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<PaymentVm>>> HandleAsync([FromQuery] GetPaymentQuery request,
                                                                                           CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<PaymentVm>>(result));
    }
}