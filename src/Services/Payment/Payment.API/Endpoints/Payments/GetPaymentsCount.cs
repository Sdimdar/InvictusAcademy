using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Payments.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Payment.API.Endpoints.Payments;

public class GetPaymentsCount:EndpointBaseAsync
    .WithRequest<GetPaymentsCountQuery>
    .WithActionResult<DefaultResponseObject<int>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetPaymentsCount(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("Payments/Count")]
    [SwaggerOperation(
        Summary = "Запрос на получение количества оплат по статусу",
        Description = "Необходимо передать PaymentStatus",
        Tags = new[] { "Payments" })
    ]

    public override async Task<ActionResult<DefaultResponseObject<int>>> HandleAsync([FromQuery]GetPaymentsCountQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<int>>(response));
    }
}