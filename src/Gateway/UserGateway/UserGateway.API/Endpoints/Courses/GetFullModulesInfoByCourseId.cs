using Ardalis.ApiEndpoints;
using AutoMapper;
using CommonStructures;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;
using UserGateway.Application.Features.Courses.Queries.GetFullModulesInfoByCourseId;

namespace UserGateway.API.Endpoints.Courses;

public class GetFullModulesInfoByCourseId : EndpointBaseAsync
    .WithRequest<GetFullByCourseIdGatewayQuery>
    .WithActionResult<DefaultResponseObject<List<ModuleInfoVm>>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<GetFullModulesInfoByCourseId> _logger;

    public GetFullModulesInfoByCourseId(IMediator mediator, IMapper mapper, ILogger<GetFullModulesInfoByCourseId> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("/Courses/GetFullModulesInfoByCourseId")]
    [SwaggerOperation(
        Summary = "Получение данных о курсе по Id, только если курс куплен",
        Description = "Необходимо передать в строке запроса  Id курса",
        Tags = new[] { "Module" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<List<ModuleInfoVm>>>> HandleAsync([FromQuery] GetFullByCourseIdGatewayQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" +
                               $"CourseId {request.CourseId}" +
                               $"UserEmail {request.UserEmail}");
        string email = HttpContext.Session.GetData("user")!.Email;
        request.UserEmail = email;
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<List<ModuleInfoVm>>>(result));
    }
}