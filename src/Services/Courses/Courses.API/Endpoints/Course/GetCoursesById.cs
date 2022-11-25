using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Course;

public class GetCoursesById:EndpointBaseAsync
    .WithRequest<GetCoursesNamesByListIdQuery>
    .WithActionResult<DefaultResponseObject<List<CoursesByIdVm>>>

{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetCoursesById(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Course/GetCoursesById")]
    [SwaggerOperation(
        Summary = "Получение курса",
        Description = "Необходимо передать в теле запроса list id курсов",
        Tags = new[] { "Course" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<List<CoursesByIdVm>>>> HandleAsync([FromBody]GetCoursesNamesByListIdQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<List<CoursesByIdVm>>>(result));
    }
}