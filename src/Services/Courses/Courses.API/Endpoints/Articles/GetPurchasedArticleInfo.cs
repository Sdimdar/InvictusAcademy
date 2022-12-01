using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Articles;

public class GetPurchasedArticleInfo : EndpointBaseAsync
    .WithRequest<GetPurchasedArticleQuery>
    .WithActionResult<DefaultResponseObject<PurchasedArticleInfoVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetPurchasedArticleInfo(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Articles/GetPurchasedArticleInfo")]
    [SwaggerOperation(
        Summary = "Получение данных о статье по eё Order, Id курса и Id модуля, " +
                  "только если курс куплен и статья открыта",
        Description = "Необходимо передать в строке запроса Order статьи, Id курса и Id модуля",
        Tags = new[] { "Articles" })
    ]
    public async override Task<ActionResult<DefaultResponseObject<PurchasedArticleInfoVm>>> HandleAsync([FromQuery] GetPurchasedArticleQuery request,
                                                                                                        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<PurchasedArticleInfoVm>>(result));
    }
}
