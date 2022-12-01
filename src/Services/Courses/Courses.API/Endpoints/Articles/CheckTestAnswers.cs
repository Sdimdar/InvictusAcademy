using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Tests.Commands;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace UserGateway.API.Endpoints.Courses;

public class CheckTestAnswers : EndpointBaseAsync
    .WithRequest<CheckTestAnswersCommand>
    .WithActionResult<DefaultResponseObject<TestResultVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CheckTestAnswers(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Tests/CheckTestAnswers")]
    [SwaggerOperation(
        Summary = "Проверка теста на базе высланных данных",
        Description = "Необходимо передать в теле запроса " +
                      "Order статьи, Id курса, Id пользователя и Id модуля и объект c ответами на тест",
        Tags = new[] { "Tests" })
    ]
    public async override Task<ActionResult<DefaultResponseObject<TestResultVm>>> HandleAsync([FromBody] CheckTestAnswersCommand request,
                                                                                              CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<TestResultVm>>(result));
    }
}
