using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Course;

public class ChangeAllModules : EndpointBaseAsync
    .WithRequest<ChangeAllModulesCommand>
    .WithActionResult<DefaultResponseObject<CourseInfoVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ChangeAllModules(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Course/ChangeAllModules")]
    [SwaggerOperation(
        Summary = "Замена всех модулей в курсе на другой список",
        Description = "Необходимо передать в теле запроса Id курса и список Id добавляемых модулей",
        Tags = new[] { "Course" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> HandleAsync([FromBody] ChangeAllModulesCommand request,
                                                                                              CancellationToken cancellationToken = default)
    {

        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<CourseInfoVm>>(result));
    }
}