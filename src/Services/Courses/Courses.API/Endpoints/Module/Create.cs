using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Modules.Commands;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Module;

public class Create : EndpointBaseAsync
    .WithRequest<CreateModuleCommand>
    .WithActionResult<DefaultResponseObject<ModuleInfoVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Create(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Modules/Create")]
    [SwaggerOperation(
        Summary = "Создание модуля",
        Description = "Для создания модуля нужно передать его название и описание, также можно сразу передать вместе с статьями",
        Tags = new[] { "Module" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> HandleAsync([FromBody] CreateModuleCommand request,
                                                                                              CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<ModuleInfoVm>>(result));
    }
}
