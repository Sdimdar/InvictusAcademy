using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Tests.Queries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;
using UserGateway.Application.Features.Courses.Queries.GetPurchasedTest;

namespace UserGateway.API.Endpoints.Courses;

public class GetPurchasedTest : EndpointBaseAsync
    .WithRequest<GetPurchasedTestGatewayQuery>
    .WithActionResult<DefaultResponseObject<List<PurchasedTestVm>>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetPurchasedTest(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Articles/GetPurchasedTest")]
    [SwaggerOperation(
        Summary = "Получение данных о тесте по Order статьи, Id курса и Id модуля, " +
                  "только если курс куплен и статья открыта",
        Description = "Необходимо передать в строке запроса Order статьи, Id курса и Id модуля",
        Tags = new[] { "Articles" })
    ]
    public async override Task<ActionResult<DefaultResponseObject<List<PurchasedTestVm>>>> HandleAsync
        ([FromQuery] GetPurchasedTestGatewayQuery request, CancellationToken cancellationToken)
    {
        GetPurchasedTestQuery query = new()
        {
            UserId = HttpContext.Session.GetData("user")!.Id,
            CourseId = request.CourseId,
            ModuleId = request.ModuleId,
            ArticleOrder = request.ArticleOrder
        };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<List<PurchasedTestVm>>>(result));
    }
}
