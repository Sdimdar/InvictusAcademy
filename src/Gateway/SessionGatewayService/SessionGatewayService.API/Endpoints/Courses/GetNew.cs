using Ardalis.ApiEndpoints;
using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Querries;
using ServicesContracts.Courses.Responses;
using SessionGatewayService.API.Extensions;
using SessionGatewayService.Application.Features.User.Querries.GetUserData;
using Swashbuckle.AspNetCore.Annotations;

namespace SessionGatewayService.API.Endpoints.Courses;

public class GetNew : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<DefaultResponceObject<CoursesVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetNew(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Courses/GetNew")]
    [SwaggerOperation(
        Summary = "Получение списка неоплаченных курсов",
        Description = "Для получения данных пользователь должен быть залогинен",
        Tags = new[] { "Courses" })
    ]
    public async override Task<ActionResult<DefaultResponceObject<CoursesVm>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            string email = HttpContext.Session.GetData("user")!.Email;
            var result = await _mediator.Send(new GetCoursesQuerry() { Email = email, Type = CourseTypes.New }, cancellationToken);
            return Ok(_mapper.Map<DefaultResponceObject<CoursesVm>>(result));
        }
        catch (Exception ex)
        {
            return Ok(_mapper.Map<DefaultResponceObject<GetUserDataVm>>(Result.Error(ex.Message)));
        }
    }
}
