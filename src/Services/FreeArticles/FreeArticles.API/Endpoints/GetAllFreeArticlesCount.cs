using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.FreeArticles.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace FreeArticles.API.Endpoints;

public class GetAllFreeArticlesCount : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<DefaultResponseObject<int>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;


    public GetAllFreeArticlesCount(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/FreeArticle/GetCount")]
    [SwaggerOperation(
        Summary = "Запрос на выгрузку количества бесплатных статей",
        Description = "Не требуется ничего вводить",
        Tags = new[] { "FreeArticle" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<int>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await _mediator.Send(new GetAllFreeArticlesCountQuery(), cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<int>>(response));
    }
}