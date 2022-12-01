using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Modules.Commands;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Module;

public class AddTest: EndpointBaseAsync
    .WithRequest<AddTestCommand>
    .WithActionResult<DefaultResponseObject<ModuleInfoVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AddTest(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Modules/AddTest")]
    [SwaggerOperation(
        Summary = "Добавление теста",
        Description = "Необходимо передать в теле запроса объект с ID модуля, порядковый номер статьи и тест",
        Tags = new[] { "Module" })
    ]

    public override async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> HandleAsync(AddTestCommand request, 
        CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<ModuleInfoVm>>(result));
    }
}