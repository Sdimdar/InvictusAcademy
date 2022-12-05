using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Modules.Queries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace UserGateway.API.Endpoints.Courses;

public class GetShortModulesInfoByCourseId : EndpointBaseAsync
    .WithRequest<GetShortCourseInfoQuery>
    .WithActionResult<DefaultResponseObject<List<ShortModuleInfoVm>>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;


    public GetShortModulesInfoByCourseId(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Modules/GetShortModulesInfoByCourseId")]
    [SwaggerOperation(
        Summary = "Получение данных о модулях по ID курса, не включая разделов",
        Description = "Необходимо передать в строке запроса  Id Курса",
        Tags = new[] { "Module" })
    ]

    public override async Task<ActionResult<DefaultResponseObject<List<ShortModuleInfoVm>>>> HandleAsync([FromQuery] GetShortCourseInfoQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<List<ShortModuleInfoVm>>>(result));
    }
}