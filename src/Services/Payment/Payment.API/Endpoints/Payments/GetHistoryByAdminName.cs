using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Payments.Queries;
using ServicesContracts.Payments.Response;
using Swashbuckle.AspNetCore.Annotations;

namespace Payment.API.Endpoints.Payments;

public class GetHistoryByAdminName:EndpointBaseAsync
    .WithRequest<GetHistoryByAdminNameQuery>
    .WithActionResult<DefaultResponseObject<List<PaymentHistoryVm>>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetHistoryByAdminName(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    [HttpGet("Payments/GetHistoryByAdminName")]
    [SwaggerOperation(
        Summary = "Запрос на получение истории действий менеджера",
        Description = "Необходимо передать admin email ",
        Tags = new[] { "Payments" })
        ]
    public override async Task<ActionResult<DefaultResponseObject<List<PaymentHistoryVm>>>> HandleAsync([FromQuery]GetHistoryByAdminNameQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<List<PaymentHistoryVm>>>(response));
    }
}