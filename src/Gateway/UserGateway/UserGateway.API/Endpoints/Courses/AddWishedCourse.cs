using Ardalis.ApiEndpoints;
using AutoMapper;
using Courses.Domain.Entities;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Commands;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;

namespace UserGateway.API.Endpoints.Courses;

public class AddWishedCourse: EndpointBaseAsync
    .WithRequest<AddToWishedCourseCommand>
    .WithActionResult<DefaultResponseObject<bool>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AddWishedCourse(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Courses/AddToWished")]
    [SwaggerOperation(
        Summary = "Добавление курса в избранное",
        Description = "Для добавления пользователь должен быть залогинен",
        Tags = new[] { "Courses" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<bool>>> HandleAsync([FromBody] AddToWishedCourseCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        int id = HttpContext.Session.GetData("user")!.Id;
        var result = await _mediator.Send(new AddToWishedCourseCommand() { UserId = id, CourseId = request.CourseId }, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<bool>>(result));
    }
}