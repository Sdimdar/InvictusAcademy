using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.FreeArticles.Models;
using ServicesContracts.FreeArticles.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace FreeArticles.API.Endpoints;

public class GetAllFreeArticles : EndpointBaseAsync
    .WithRequest<GetAllFreeArticlesQuery>
    .WithActionResult<DefaultResponseObject<AllFreeArticlesVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetAllFreeArticles(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/FreeArticle/GetAll")]
    [SwaggerOperation(
        Summary = "Получение данных бесплатных статей",
        Description = "Для пагинации требуется вести в строку номер страницы, строка фильтра может быть пустой",
        Tags = new[] { "FreeArticle" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<AllFreeArticlesVm>>> HandleAsync([FromQuery] GetAllFreeArticlesQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<AllFreeArticlesVm>>(response));
    }
}