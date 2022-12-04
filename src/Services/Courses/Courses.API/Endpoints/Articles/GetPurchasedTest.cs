using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Tests.Queries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Articles;

public class GetPurchasedTest : EndpointBaseAsync
    .WithRequest<GetPurchasedTestQuery>
    .WithActionResult<DefaultResponseObject<List<PurchasedTestVm>>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetPurchasedTest(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Articles/GetPurchasedTestInfo")]
    [SwaggerOperation(
        Summary = "Получение данных о тесте по Order статьи, Id курса, Id модуля и Id пользователя, " +
                  "только если курс куплен и статья открыта",
        Description = "Необходимо передать в строке запроса Order статьи, Id курса, Id модуля и Id пользователя",
        Tags = new[] { "Articles" })
    ]
    public async override Task<ActionResult<DefaultResponseObject<List<PurchasedTestVm>>>> HandleAsync([FromQuery] GetPurchasedTestQuery request,
                                                                                                       CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<List<PurchasedTestVm>>>(result));
    }
}
