using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Course;

public class InsertModules : EndpointBaseAsync
    .WithRequest<InsertModulesCommand>
    .WithActionResult<DefaultResponseObject<CourseInfoVm>>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public InsertModules(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Course/InsertModules")]
    [SwaggerOperation(
        Summary = "Добавление модулей в курс",
        Description = "Необходимо передать в теле запроса Id курса, список Id добавляемых в модуль" +
                      " и Index начиная с которого вставятся модуля, Если index < 0, то список добавится в конец",
        Tags = new[] { "Course" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> HandleAsync([FromBody] InsertModulesCommand request,
                                                                                              CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<CourseInfoVm>>(result));
    }
}
