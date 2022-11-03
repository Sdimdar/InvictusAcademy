using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Course;

public class Delete : EndpointBaseAsync
    .WithRequest<DeleteCourseCommand>
    .WithActionResult<DefaultResponseObject<bool>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Delete(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Course/Delete")]
    [SwaggerOperation(
        Summary = "Удаление курса",
        Description = "Необходимо передать в теле ID курса",
        Tags = new[] { "Course" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<bool>>> HandleAsync([FromBody] DeleteCourseCommand request,
                                                                                      CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<bool>>(result));
    }
}
