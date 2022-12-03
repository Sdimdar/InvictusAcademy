using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Course;

public class GetCourseById : EndpointBaseAsync
    .WithRequest<GetCourseByIdQuery>
    .WithActionResult<DefaultResponseObject<CourseByIdVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetCourseById(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Course/GetCourse")]
    [SwaggerOperation(
        Summary = "Получение курса",
        Description = "Необходимо передать в теле запроса Id курса",
        Tags = new[] { "Course" })
    ]

    public override async Task<ActionResult<DefaultResponseObject<CourseByIdVm>>> HandleAsync([FromQuery] GetCourseByIdQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<CourseByIdVm>>(result));
    }
}