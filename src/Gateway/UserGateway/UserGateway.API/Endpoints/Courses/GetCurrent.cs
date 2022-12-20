using Ardalis.ApiEndpoints;
using AutoMapper;
using CommonStructures;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;
using UserGateway.Application.Features.Courses.Queries.GetCourses;

namespace UserGateway.API.Endpoints.Courses;

public class GetCurrent : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<DefaultResponseObject<CoursesVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCurrent> _logger;

    public GetCurrent(IMediator mediator, IMapper mapper, ILogger<GetCurrent> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("/Courses/GetCurrent")]
    [SwaggerOperation(
        Summary = "Получение списка текущих курсов",
        Description = "Для получения данных пользователь должен быть залогинен",
        Tags = new[] { "Courses" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<CoursesVm>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        string email = HttpContext.Session.GetData("user")!.Email;
        var result = await _mediator.Send(new GetGatewayCoursesQuery() { Email = email, Type = CourseTypes.Current }, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<CoursesVm>>(result));
    }
}
