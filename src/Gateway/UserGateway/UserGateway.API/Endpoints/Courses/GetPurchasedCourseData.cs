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

namespace UserGateway.API.Endpoints.Courses;

public class GetPurchasedCourseData : EndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<DefaultResponseObject<PurchasedCourseInfoVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPurchasedCourseData> _logger;

    public GetPurchasedCourseData(IMediator mediator, IMapper mapper, ILogger<GetPurchasedCourseData> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("/Courses/GetPurchasedCourseData")]
    [SwaggerOperation(
        Summary = "Получение данных о курсе по Id, только если курс куплен",
        Description = "Необходимо передать в строке запроса  Id курса",
        Tags = new[] { "Courses" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<PurchasedCourseInfoVm>>> HandleAsync([FromQuery] int courseId,
                                                                                                       CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" +
                               $"courseID {courseId}");
        int userId = HttpContext.Session.GetData("user")!.Id;
        GetPurchasedCourseDataQuery query = new()
        {
            CourseId = courseId,
            UserId = userId
        };
        var result = await _mediator.Send(query, cancellationToken);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}" +
                               $"Errors {result.Errors}" +
                               $"ValidationErrors {result.ValidationErrors}" +
                               $"IsSuccess {result.IsSuccess}" +
                               $"Course Id {result.Value.Id}" +
                               $"Course Name {result.Value.Name}" +
                               $"Modules.Count {result.Value.Modules.Count}" +
                               $"Course Description {result.Value.Description}" +
                               $"PassingTime {result.Value.PassingTime}" +
                               $"");
        return Ok(_mapper.Map<DefaultResponseObject<PurchasedCourseInfoVm>>(result));
    }
}
