using Ardalis.ApiEndpoints;
using AutoMapper;
using Courses.Domain.Entities.CourseInfo;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Course;

public class EditCourse : EndpointBaseAsync
    .WithRequest<EditCourseCommand>
    .WithActionResult<DefaultResponseObject<CourseInfoDbModel>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;


    public EditCourse(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("/Course/Edit")]
    [SwaggerOperation(
        Summary = "Редакитрование курса",
        Description = "Необходимо передать в теле запроса данные по редактируемому курсу + id",
        Tags = new[] { "Course" })
    ]

    public override async Task<ActionResult<DefaultResponseObject<CourseInfoDbModel>>> HandleAsync(EditCourseCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<CourseInfoDbModel>>(result));
    }
}