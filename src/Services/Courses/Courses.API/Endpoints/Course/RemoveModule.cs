using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Commands;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Course;

public class RemoveModule : EndpointBaseAsync
    .WithRequest<RemoveModuleCommand>
    .WithActionResult<DefaultResponseObject<CourseInfoVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public RemoveModule(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Course/RemoveModule")]
    [SwaggerOperation(
        Summary = "Удаление модуля из курса",
        Description = "Необходимо передать в теле запроса Id курса, Id удяляемого модуля",
        Tags = new[] { "Course" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> HandleAsync([FromBody] RemoveModuleCommand request,
                                                                                              CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<CourseInfoVm>>(result));
    }
}
