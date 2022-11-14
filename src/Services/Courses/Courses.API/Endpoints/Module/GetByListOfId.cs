using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Modules.Queries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Module;

public class GetByListOfId : EndpointBaseAsync
    .WithRequest<GetModulesByListOfIdQuery>
    .WithActionResult<DefaultResponseObject<List<ModuleInfoVm>>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetByListOfId(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Modules/GetByListOfId")]
    [SwaggerOperation(
        Summary = "Получение данных о модулях по списку их ID",
        Description = "Необходимо передать в строке запроса список Id модулей",
        Tags = new[] { "Module" })
    ]
    public async override Task<ActionResult<DefaultResponseObject<List<ModuleInfoVm>>>> HandleAsync([FromQuery]GetModulesByListOfIdQuery request,
                                                                                                    CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<List<ModuleInfoVm>>>(result));
    }
}
