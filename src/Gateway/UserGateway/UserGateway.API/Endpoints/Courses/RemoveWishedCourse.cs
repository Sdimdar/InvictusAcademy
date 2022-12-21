using Ardalis.ApiEndpoints;
using AutoMapper;
using CommonStructures;
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
    private readonly ILogger<RemoveWishedCourse> _logger;

    public RemoveWishedCourse(IMediator mediator, IMapper mapper, ILogger<RemoveWishedCourse> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }
    
    [HttpPost("/Courses/RemoveFromWished")]
    [SwaggerOperation(
        Summary = "Удаление курса из избранных",
        Description = "Для удаления пользователь должен быть залогинен",
        Tags = new[] { "Courses" })
    ]

    public override async Task<ActionResult<DefaultResponseObject<bool>>> HandleAsync(RemoveFromWishedCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" +
                               $"CourseId {request.CourseId}" +
                               $"UserId {request.UserId}");
        int id = HttpContext.Session.GetData("user")!.Id;
        var result = await _mediator.Send(new RemoveFromWishedCommand() { UserId = id, CourseId = request.CourseId }, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<bool>>(result));
    }
}