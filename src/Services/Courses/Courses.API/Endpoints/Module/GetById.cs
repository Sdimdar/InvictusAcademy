using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Modules.Queries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Module;

public class GetById : EndpointBaseAsync
    .WithRequest<GetModuleByIdQuery>
    .WithActionResult<DefaultResponseObject<ModuleInfoVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetById(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Module/GetById")]
    [SwaggerOperation(
        Summary = "Получение данных о модуле по его ID",
        Description = "Необходимо передать в строке запроса Id модуля",
        Tags = new[] { "Module" })
    ]
    public async override Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> HandleAsync(GetModuleByIdQuery request,
                                                                                              CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<ModuleInfoVm>>(result));
    }
}
