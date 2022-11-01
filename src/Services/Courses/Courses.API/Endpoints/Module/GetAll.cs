using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Modules.Queries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Module;

public class GetAll : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<DefaultResponseObject<List<ModuleInfoVm>>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetAll(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Module/GetAll")]
    [SwaggerOperation(
        Summary = "Получение данных о всех модулях",
        Description = "Получение данных о всех модулях в базе данных",
        Tags = new[] { "Module" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<List<ModuleInfoVm>>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetAllModulesQuerry(), cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<List<ModuleInfoVm>>>(result));
    }
}
