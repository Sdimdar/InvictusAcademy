using Ardalis.ApiEndpoints;
using AutoMapper;
using CommonStructures;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Requests.Commands;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;

namespace UserGateway.API.Endpoints.User;

public class Edit : EndpointBaseAsync
    .WithRequest<EditCommand>
    .WithActionResult<DefaultResponseObject<string>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<Edit> _logger;

    public Edit(IMediator mediator, IMapper mapper, ILogger<Edit> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost("User/Edit")]
    [SwaggerOperation(
        Summary = "Изменение данных пользователя",
        Description = "Для изменения данных пользователь должен быть залогинен, необходимо ввести данные в виде JSON",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<string>>> HandleAsync([FromBody] EditCommand request,
                                                                                  CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" +
                               $"Citizenship {request.Citizenship}" +
                               $"Email {request.Email}" +
                               $"FirstName {request.FirstName}" +
                               $"InstagramLink {request.InstagramLink}" +
                               $"LastName {request.LastName}" +
                               $"MiddleName {request.MiddleName}" +
                               $"PhoneNumber {request.PhoneNumber}");
        request.Email = HttpContext.Session.GetData("user").Email;
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<string>>(response));
    }
}
