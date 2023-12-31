﻿using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.FreeArticles.Models;
using ServicesContracts.FreeArticles.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace FreeArticles.API.Endpoints;

public class GetFreeArticleData : EndpointBaseAsync
    .WithRequest<GetFreeArticleDataQuery>
    .WithActionResult<DefaultResponseObject<FreeArticleVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetFreeArticleData(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/FreeArticle/GetFreeArticleData")]
    [SwaggerOperation(
        Summary = "Получение данных о бесплатной статье",
        Description = "Для получения данных о пользователе необходимо передать его id через параметры в строке",
        Tags = new[] { "FreeArticle" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<FreeArticleVm>>> HandleAsync([FromQuery] GetFreeArticleDataQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<FreeArticleVm>>(response));
    }
}