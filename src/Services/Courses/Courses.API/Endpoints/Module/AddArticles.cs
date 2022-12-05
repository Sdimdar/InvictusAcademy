using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Modules.Commands;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Module;

public class AddArticles : EndpointBaseAsync
    .WithRequest<AddArticlesCommand>
    .WithActionResult<DefaultResponseObject<ModuleInfoVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AddArticles(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Modules/AddArticles")]
    [SwaggerOperation(
        Summary = "Добавление статей в модуль",
        Description = "Необходимо передать в теле запроса объект с ID модуля и список объектов статей",
        Tags = new[] { "Module" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> HandleAsync([FromBody] AddArticlesCommand request,
                                                                                              CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<ModuleInfoVm>>(result));
    }
}
