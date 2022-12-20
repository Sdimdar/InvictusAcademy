using Ardalis.ApiEndpoints;
using AutoMapper;
using CommonStructures;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Tests.Commands;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;
using UserGateway.Application.Features.Courses.Commands.CheckTestAnswers;

namespace UserGateway.API.Endpoints.Courses;

public class CheckTestAnswers : EndpointBaseAsync
    .WithRequest<CheckTestAnswersGatewayCommand>
    .WithActionResult<DefaultResponseObject<TestResultVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<CheckTestAnswers> _logger;

    public CheckTestAnswers(IMediator mediator, IMapper mapper, ILogger<CheckTestAnswers> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost("/Tests/CheckTestAnswers")]
    [SwaggerOperation(
        Summary = "Проверка теста на базе высланных данных",
        Description = "Необходимо передать в теле запроса Order статьи, Id курса и Id модуля и объект c ответами на тест",
        Tags = new[] { "Tests" })
    ]
    public async override Task<ActionResult<DefaultResponseObject<TestResultVm>>> HandleAsync([FromBody] CheckTestAnswersGatewayCommand request,
                                                                                              CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" +
                               $"CourseId {request.CourseId}" +
                               $"ModuleId {request.ModuleId}" +
                               $"ArticleOrder {request.ArticleOrder}" +
                               $"Answers Count {request.Answers.Count}");
        CheckTestAnswersCommand command = new()
        {
            UserId = HttpContext.Session.GetData("user")!.Id,
            Answers = request.Answers,
            ArticleOrder = request.ArticleOrder,
            CourseId = request.CourseId,
            ModuleId = request.ModuleId
        };
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<TestResultVm>>(result));
    }
}
