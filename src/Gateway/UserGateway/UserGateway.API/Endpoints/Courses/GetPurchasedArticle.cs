using Ardalis.ApiEndpoints;
using AutoMapper;
using CommonStructures;
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
    private readonly ILogger<GetPurchasedArticle> _logger;

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
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" +
                               $"ArticleOrder {request.ArticleOrder}" +
                               $"CourseId {request.CourseId}" +
                               $"ModuleId {request.ModuleId}");
        GetPurchasedArticleQuery query = new()
        {
            UserId = HttpContext.Session.GetData("user")!.Id,
            CourseId = request.CourseId,
            ModuleId = request.ModuleId,
            ArticleOrder = request.ArticleOrder
        };
        var result = await _mediator.Send(query, cancellationToken);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}" +
                               $"Errors {result.Errors}" +
                               $"ValidationErrors {result.ValidationErrors}" +
                               $"IsSuccess {result.IsSuccess}" +
                               $"Articles Count {result.Value.Articles.Count}" +
                               $"Order {result.Value.Order}" +
                               $"Text {result.Value.Text}" +
                               $"Title {result.Value.Title}" +
                               $"IsCompleted {result.Value.IsCompleted}" +
                               $"ModuleInfo.Id {result.Value.ModuleInfo.Id}" +
                               $"ModuleInfo.Title {result.Value.ModuleInfo.Title}" +
                               $"ModuleInfo.ShortDescription {result.Value.ModuleInfo.ShortDescription}" +
                               $"");
        return Ok(_mapper.Map<DefaultResponseObject<PurchasedArticleInfoVm>>(result));
    }
}
