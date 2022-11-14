using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;
using UserGateway.Application.Features.Courses.Queries.GetFullModulesInfoByCourseId;

namespace UserGateway.API.Endpoints.Courses;

public class GetFullModulesInfoByCourseId:EndpointBaseAsync
    .WithRequest<GetFullByCourseIdGatewayQuery>
    .WithActionResult<DefaultResponseObject<List<ModuleInfoVm>>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetFullModulesInfoByCourseId(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Courses/GetFullModulesInfoByCourseId")]
    [SwaggerOperation(
        Summary = "Получение данных о курсе по Id, только если курс куплен",
        Description = "Необходимо передать в строке запроса  Id курса",
        Tags = new[] { "Module" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<List<ModuleInfoVm>>>> HandleAsync([FromQuery]GetFullByCourseIdGatewayQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        string email = HttpContext.Session.GetData("user")!.Email;
        request.UserEmail = email;
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<List<ModuleInfoVm>>>(result));
    }
}