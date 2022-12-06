using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Course;

public class GetStartedCourses: EndpointBaseAsync
.WithRequest<GetStartedCoursesQuery>
.WithActionResult<DefaultResponseObject<List<StartedCourseInfoVm>>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetStartedCourses(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/Courses/GetStartedCourses")]
    [SwaggerOperation(
        Summary = "Получение дат начала и завершения купленного курса",
        Description = "Необходимо передать id User и id course который куплен.",
        Tags = new[] { "Course" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<List<StartedCourseInfoVm>>>> HandleAsync([FromBody]GetStartedCoursesQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<CoursesVm>>(result));
    }
}