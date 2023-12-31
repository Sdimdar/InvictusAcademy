﻿using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Course;

public class Create : EndpointBaseAsync
    .WithRequest<CreateCourseCommand>
    .WithActionResult<DefaultResponseObject<CourseVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Create(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Course/Create")]
    [SwaggerOperation(
        Summary = "Создание курса",
        Description = "Необходимо передать в теле запроса данные по новому курсу",
        Tags = new[] { "Course" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<CourseVm>>> HandleAsync([FromBody] CreateCourseCommand request,
                                                                                               CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<CourseVm>>(result));
    }
}
