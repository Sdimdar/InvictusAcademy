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

public class GetAll : EndpointBaseAsync
    .WithRequest<GetAllFreeArticlesQuery>
    .WithActionResult<DefaultResponseObject<AllFreeArticlesVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetAll(IMediator mediator, IMapper mapper)
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
        var email = HttpContext.Session.GetData("user").Email;
        if (email is null)
        {
            throw new UnauthorizedAccessException("User is not authorized");
        }
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<AllFreeArticlesVm>>(result));
    }
}