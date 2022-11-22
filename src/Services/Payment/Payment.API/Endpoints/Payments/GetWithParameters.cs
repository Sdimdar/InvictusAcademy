using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Payments.Models;
using ServicesContracts.Payments.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Payment.API.Endpoints.Payments;

public class GetWithParameters : EndpointBaseAsync
    .WithRequest<GetPaymentsWithParametersQuery>
    .WithActionResult<DefaultResponseObject<PaymentsVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetWithParameters(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Payments/GetWithParameters")]
    [SwaggerOperation(
        Summary = "Получение платежей",
        Description = "Необходимо передать в сроке запроса при необходимости Id пользователя или Id курса," +
                      "а также можно передать тип запроса на оплату",
        Tags = new[] { "Payments" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<PaymentsVm>>> HandleAsync([FromQuery] GetPaymentsWithParametersQuery request, 
                                                                                           CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<List<PaymentsVm>>>(result));
    }
}