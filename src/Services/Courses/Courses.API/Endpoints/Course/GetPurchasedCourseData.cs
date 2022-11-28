using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Course;

public class GetPurchasedCourseData : EndpointBaseAsync
    .WithRequest<GetPurchasedCourseDataQuery>
    .WithActionResult<DefaultResponseObject<PurchasedCourseInfoVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetPurchasedCourseData(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("Courses/GetPurchasedCourseData")]
    [SwaggerOperation(
        Summary = "Получение данных о курсе по Id, только если курс куплен",
        Description = "Необходимо передать в строке запроса  Id курса и Id юзера",
        Tags = new[] { "Courses" })
    ]
    public async override Task<ActionResult<DefaultResponseObject<PurchasedCourseInfoVm>>> HandleAsync([FromQuery] GetPurchasedCourseDataQuery request,
                                                                                                       CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<PurchasedCourseInfoVm>>(result));
    }
}
