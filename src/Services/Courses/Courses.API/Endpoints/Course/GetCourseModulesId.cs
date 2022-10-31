using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Querries;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Course;

public class GetCourseModulesId : EndpointBaseAsync
    .WithRequest<GetCourseModulesIdQuerry>
    .WithActionResult<DefaultResponseObject<List<int>>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetCourseModulesId(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Course/GetModules")]
    [SwaggerOperation(
        Summary = "Получение списка из ID модулей в курсе",
        Description = "Необходимо передать в строке запроса ID курса",
        Tags = new[] { "Course" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<List<int>>>> HandleAsync([FromQuery] GetCourseModulesIdQuerry request,
                                                                                           CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<List<int>>>(result));
    }
}
