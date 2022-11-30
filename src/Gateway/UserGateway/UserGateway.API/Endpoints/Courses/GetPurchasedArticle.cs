using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;
using UserGateway.Application.Features.Courses.Queries.GetPurchasedArticle;

namespace UserGateway.API.Endpoints.Courses;

public class GetPurchasedArticle : EndpointBaseAsync
    .WithRequest<GetPurchasedArticleGatewayQuery>
    .WithActionResult<DefaultResponseObject<PurchasedArticleInfoVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetPurchasedArticle(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Articles/GetPurchasedArticle")]
    [SwaggerOperation(
        Summary = "Получение данных о статье по eё Order, Id курса и Id модуля, " +
                  "только если курс куплен и статья открыта",
        Description = "Необходимо передать в строке запроса Order статьи, Id курса и Id модуля",
        Tags = new[] { "Articles" })
    ]
    public async override Task<ActionResult<DefaultResponseObject<PurchasedArticleInfoVm>>> HandleAsync([FromQuery] GetPurchasedArticleGatewayQuery request,
                                                                                                        CancellationToken cancellationToken)
    {
        GetPurchasedArticleQuery query = new()
        {
            UserId = HttpContext.Session.GetData("user")!.Id,
            CourseId = request.CourseId,
            ModuleId = request.ModuleId,
            ArticleOrder = request.ArticleOrder
        };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<PurchasedArticleInfoVm>>(result));
    }
}
