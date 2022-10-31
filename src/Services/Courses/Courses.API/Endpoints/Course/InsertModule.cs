using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Commands;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Course;

public class InsertModule : EndpointBaseAsync
    .WithRequest<InsertModuleCommand>
    .WithActionResult<DefaultResponseObject<CourseInfoVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public InsertModule(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Course/InsertModule")]
    [SwaggerOperation(
        Summary = "Добавление модуля в курс",
        Description = "Необходимо передать в теле запроса Id курса, Id добавляемого модуля" +
                      " и Index куда вставляется модуль, Если index < 0, то модуль добавится в конец",
        Tags = new[] { "Course" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> HandleAsync([FromBody] InsertModuleCommand request,
                                                                                              CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<CourseInfoVm>>(result));
    }
}
