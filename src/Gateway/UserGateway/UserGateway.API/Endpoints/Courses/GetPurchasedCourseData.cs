using Ardalis.ApiEndpoints;
using AutoMapper;
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

    public GetPurchasedCourseData(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
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
        int userId = HttpContext.Session.GetData("user")!.Id;
        GetPurchasedCourseDataQuery query = new()
        {
            CourseId = courseId,
            UserId = userId
        };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<PurchasedCourseInfoVm>>(result));
    }
}
