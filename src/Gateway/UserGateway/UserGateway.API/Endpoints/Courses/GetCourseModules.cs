using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Requests.Modules.Queries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace UserGateway.API.Endpoints.Courses;

public class GetCourseModules : EndpointBaseAsync
    .WithRequest<GetModuleByIdQuery>
    .WithActionResult<DefaultResponseObject<CoursesVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    [HttpGet("/Course/GetModules")]
    [SwaggerOperation(
        Summary = "Получение списка из ID модулей в курсе",
        Description = "Необходимо передать в строке запроса ID курса",
        Tags = new[] { "Modules" })
    ]

    public async override Task<ActionResult<DefaultResponseObject<CoursesVm>>> HandleAsync(GetModuleByIdQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<CoursesVm>>(result));
    }
}