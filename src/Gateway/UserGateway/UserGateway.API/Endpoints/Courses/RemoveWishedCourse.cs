using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Commands;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;

namespace UserGateway.API.Endpoints.Courses;

public class RemoveWishedCourse : EndpointBaseAsync
    .WithRequest<RemoveFromWishedCommand>
    .WithActionResult<DefaultResponseObject<bool>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public RemoveWishedCourse(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("/Courses/RemoveFromWished")]
    [SwaggerOperation(
        Summary = "Удаление курса из избранных",
        Description = "Для удаления пользователь должен быть залогинен",
        Tags = new[] { "Courses" })
    ]

    public override async Task<ActionResult<DefaultResponseObject<bool>>> HandleAsync(RemoveFromWishedCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        int id = HttpContext.Session.GetData("user")!.Id;
        var result = await _mediator.Send(new RemoveFromWishedCommand() { UserId = id, CourseId = request.CourseId }, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<bool>>(result));
    }
}