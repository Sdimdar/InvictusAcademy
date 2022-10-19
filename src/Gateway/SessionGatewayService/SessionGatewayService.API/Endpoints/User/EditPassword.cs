using Ardalis.ApiEndpoints;
using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Requests.Commands;
using SessionGatewayService.API.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace SessionGatewayService.API.Endpoints.User;

public class EditPassword : EndpointBaseAsync
    .WithRequest<EditPasswordCommand>
    .WithActionResult<DefaultResponceObject<string>>
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
    public async override Task<ActionResult<DefaultResponceObject<string>>> HandleAsync([FromBody] EditPasswordCommand request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            string email = HttpContext.Session.GetData("user")!.Email;
            request.Email = email;
            var responce = await _mediator.Send(request, cancellationToken);
            return Ok(_mapper.Map<DefaultResponceObject<string>>(responce));
        }
        catch (Exception ex)
        {
            return Ok(_mapper.Map<DefaultResponceObject<string>>(Result.Error("User is not Autorized")));
        }
    }
}