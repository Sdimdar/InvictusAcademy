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
using UserGateway.Application.Features.User.Querries.GetUserData;

namespace UserGateway.API.Endpoints.Courses;

public class GetCompleted : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<DefaultResponseObject<CoursesVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetCompleted(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Courses/GetCompleted")]
    [SwaggerOperation(
        Summary = "Получение списка завершённых курсов",
        Description = "Для получения данных пользователь должен быть залогинен",
        Tags = new[] { "Courses" })
    ]
    public async override Task<ActionResult<DefaultResponseObject<CoursesVm>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            string email = HttpContext.Session.GetData("user")!.Email;
            var result = await _mediator.Send(new GetCoursesQuerry() { Email = email, Type = CourseTypes.Completed }, cancellationToken);
            return Ok(_mapper.Map<DefaultResponseObject<CoursesVm>>(result));
        }
        catch (Exception ex)
        {
            return Ok(_mapper.Map<DefaultResponseObject<GetUserDataVm>>(Result.Error(ex.Message)));
        }
    }
}
