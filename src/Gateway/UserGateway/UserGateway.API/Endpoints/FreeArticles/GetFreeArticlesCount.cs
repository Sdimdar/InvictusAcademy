using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.FreeArticles.Models;
using ServicesContracts.FreeArticles.Queries;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;

namespace UserGateway.API.Endpoints.FreeArticles;

public class GetFreeArticlesCount : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<DefaultResponseObject<AllFreeArticlesVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetFreeArticlesCount(IMediator mediator, IMapper mapper)
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
    public override async Task<ActionResult<DefaultResponseObject<AllFreeArticlesVm>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var email = HttpContext.Session.GetData("user").Email;
        if (email is null)
        {
            throw new UnauthorizedAccessException("User is not authorized");
        }
        var response = await _mediator.Send(new GetAllFreeArticlesCountQuery(), cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<int>>(response));
    }
}