using Ardalis.ApiEndpoints;
using AutoMapper;
using CommonStructures;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace UserGateway.API.Endpoints.Courses;

public class GetCourseById : EndpointBaseAsync
    .WithRequest<GetCourseByIdQuery>
    .WithActionResult<DefaultResponseObject<CourseByIdVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCourseById> _logger;

    public GetCourseById(IMediator mediator, IMapper mapper, ILogger<GetCourseById> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("/Courses/GetById")]
    [SwaggerOperation(
        Summary = "Получение данных о курсе по его Id",
        Description = "Для получения данных пользователь должен быть залогинен",
        Tags = new[] { "Courses" })
    ]

    public override async Task<ActionResult<DefaultResponseObject<CourseByIdVm>>> HandleAsync([FromQuery] GetCourseByIdQuery request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" +
                               $"Id {request.Id}");
        var result = await _mediator.Send(request, cancellationToken);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}" +
                               $"Errors {result.Errors}" +
                               $"ValidationErrors {result.ValidationErrors}" +
                               $"IsSuccess {result.IsSuccess}" +
                               $"Course Id {result.Value.Id}" +
                               $"Course Name {result.Value.Name}" +
                               $"Course Cost {result.Value.Cost}" +
                               $"Course Description {result.Value.Description}" +
                               $"IsActive {result.Value.IsActive}" +

                               $"");
        return Ok(_mapper.Map<DefaultResponseObject<CourseByIdVm>>(result));
    }
}