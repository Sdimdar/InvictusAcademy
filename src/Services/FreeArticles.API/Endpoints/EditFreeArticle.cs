using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.FreeArticles.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace FreeArticles.API.Endpoints;

public class EditFreeArticle : EndpointBaseAsync
    .WithRequest<EditFreeArticleCommand>
    .WithActionResult<DefaultResponseObject<string>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public EditFreeArticle(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/FreeArticle/Edit")]
    [SwaggerOperation(
        Summary = "Редактирование бесплатной статьи",
        Description = "Необходимо передать в теле запроса поля",
        Tags = new[] { "FreeArticle" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<string>>> HandleAsync(EditFreeArticleCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<string>>(response));
    }
}