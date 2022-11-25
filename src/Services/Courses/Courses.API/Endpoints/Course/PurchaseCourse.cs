using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Endpoints.Course;

public class PurchaseCourse : EndpointBaseAsync
    .WithRequest<PurchaseCourseCommand>
    .WithActionResult<DefaultResponseObject<bool>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public PurchaseCourse(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("/Course/Purchase")]
    [SwaggerOperation(
        Summary = "Добавление информации в бд о том что курс успешно куплен",
        Description = "Необходимо передать в теле запроса Id курса и Id пользователя",
        Tags = new[] { "Course" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<bool>>> HandleAsync(PurchaseCourseCommand request, 
                                                                                      CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<bool>>(result));
    }
}