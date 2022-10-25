using Ardalis.ApiEndpoints;
using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Requests.Commands;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;

namespace UserGateway.API.Endpoints.User;

public class EditPassword : EndpointBaseAsync
    .WithRequest<EditPasswordCommand>
    .WithActionResult<DefaultResponseObject<string>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public EditPassword(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("User/EditPassword")]
    [SwaggerOperation(
        Summary = "Изменение пароля пользователя",
        Description = "Для изменения данных пользователь должен быть залогинен, необходимо ввести данные в виде JSON",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<string>>> HandleAsync([FromBody] EditPasswordCommand request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            string email = HttpContext.Session.GetData("user")!.Email;
            request.Email = email;
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(_mapper.Map<DefaultResponseObject<string>>(response));
        }
        catch (Exception ex)
        {
            return Ok(_mapper.Map<DefaultResponseObject<string>>(Result.Error("User is not Autorized")));
        }
    }
}