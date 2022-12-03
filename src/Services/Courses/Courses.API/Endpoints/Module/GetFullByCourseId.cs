using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Modules.Queries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Module;

public class GetFullByCourseId : EndpointBaseAsync
    .WithRequest<GetFullByCourseIdQuery>
    .WithActionResult<DefaultResponseObject<List<ModuleInfoVm>>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetFullByCourseId(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Modules/GetFullByCourseId")]
    [SwaggerOperation(
        Summary = "Получение данных о курсе по Id, только если курс приобретен",
        Description = "Необходимо передать в строке запроса  Id курса",
        Tags = new[] { "Module" })
    ]

    public override async Task<ActionResult<DefaultResponseObject<List<ModuleInfoVm>>>> HandleAsync([FromQuery] GetFullByCourseIdQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<List<ModuleInfoVm>>>(response));
    }
}