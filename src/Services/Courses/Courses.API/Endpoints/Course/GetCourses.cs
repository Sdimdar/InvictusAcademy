using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Course;

public class GetCourses : EndpointBaseAsync
    .WithRequest<GetCoursesQuery>
    .WithActionResult<DefaultResponseObject<CoursesVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;


    public GetCourses(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Courses/GetCourses")]
    [SwaggerOperation(
        Summary = "Получение курсов по типу",
        Description = "Необходимо передать в теле запроса данные об Id пользователя, а также тип запрашиваемых курсов. " +
                      "Что бы получить все активные курсы для неавторизованных, UserId не указывать, CourseType = 0",
        Tags = new[] { "Course" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<CoursesVm>>> HandleAsync(GetCoursesQuery request,
                                                                                           CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<CoursesVm>>(result));
    }
}
