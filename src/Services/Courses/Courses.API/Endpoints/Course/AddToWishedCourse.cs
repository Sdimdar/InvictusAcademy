using Ardalis.ApiEndpoints;
using AutoMapper;
using Courses.Domain.Entities;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Course;

public class AddToWishedCourse : EndpointBaseAsync
    .WithRequest<AddToWishedCourseCommand>
    .WithActionResult<DefaultResponseObject<bool>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AddToWishedCourse(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("/Course/Wished")]
    [SwaggerOperation(
        Summary = "Добавление курса в избранное",
        Description = "Необходимо передать в теле запроса Id курса и Id пользователя",
        Tags = new[] { "Course" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<bool>>> HandleAsync(AddToWishedCourseCommand request, 
        CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<bool>>(result));
    }
}