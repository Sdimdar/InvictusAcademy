﻿using Ardalis.ApiEndpoints;
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

public class GetNew : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<DefaultResponseObject<CoursesVm>>
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
    public override async Task<ActionResult<DefaultResponseObject<CoursesVm>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        string email = HttpContext.Session.GetData("user")!.Email;
        var result = await _mediator.Send(new GetGatewayCoursesQuery() { Email = email, Type = CourseTypes.New }, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<CoursesVm>>(result));
    }
}