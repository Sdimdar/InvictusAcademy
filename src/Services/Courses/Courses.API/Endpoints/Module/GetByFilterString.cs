using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Modules.Queries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Module;

public class GetByFilterString : EndpointBaseAsync
    .WithRequest<GetModulesByFilterStringQuery>
    .WithActionResult<DefaultResponseObject<List<ModuleInfoVm>>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetByFilterString(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Module/GetByFilterString")]
    [SwaggerOperation(
        Summary = "Получение данных о модулях которые подходят под строку фильтрации",
        Description = "Необходимо передать в строке строку фильтрации",
        Tags = new[] { "Module" })
    ]
    public async override Task<ActionResult<DefaultResponseObject<List<ModuleInfoVm>>>> HandleAsync(GetModulesByFilterStringQuery request,
                                                                                                    CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<List<ModuleInfoVm>>>(result));
    }
}
