using Ardalis.ApiEndpoints;
using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Querries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;
using UserGateway.Application.Features.Courses.Queries.GetCourses;
using UserGateway.Application.Features.User.Querries.GetUserData;

namespace UserGateway.API.Endpoints.Courses;

public class GetWished : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<DefaultResponseObject<CoursesVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetWished(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Courses/GetWished")]
    [SwaggerOperation(
        Summary = "Получение списка желаемых курсов",
        Description = "Для получения данных пользователь должен быть залогинен",
        Tags = new[] { "Courses" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<CoursesVm>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            string email = HttpContext.Session.GetData("user")!.Email;
            var result = await _mediator.Send(new GetGatewayCoursesQuery() { Email = email, Type = CourseTypes.Wished }, cancellationToken);
            return Ok(_mapper.Map<DefaultResponseObject<CoursesVm>>(result));
        }
        catch (Exception ex)
        {
            return Ok(_mapper.Map<DefaultResponseObject<GetUserDataVm>>(Result.Error(ex.Message)));
        }
    }
}
