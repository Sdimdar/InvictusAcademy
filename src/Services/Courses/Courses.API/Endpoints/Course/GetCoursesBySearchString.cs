using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Course;

public class GetCoursesBySearchString : EndpointBaseAsync
    .WithRequest<string>
    .WithActionResult<DefaultResponseObject<CoursesVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetCoursesBySearchString(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet("/Course/GetCoursesByFilterString")]
    [SwaggerOperation(
        Summary = "Получение курсов по поисковой строке",
        Description = "Необходимо передать в теле запроса строку поиска",
        Tags = new[] { "Course" })
    ]
    public async override Task<ActionResult<DefaultResponseObject<CoursesVm>>> HandleAsync([FromQuery] string request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(new GetCoursesBySearchStringCommand() {SearchString = request},
            cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<CoursesVm>>(result));
    }
}