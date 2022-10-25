using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Responses;
using Swashbuckle.AspNetCore.Annotations;
using User.Application.Features.Users.Queries.GetUsersData;

namespace User.API.Endpoints.User;

public class GetAllRegisteredUsersData : EndpointBaseAsync
    .WithRequest<GetAllUsersCommand>
    .WithActionResult<DefaultResponseObject<UsersVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetAllRegisteredUsersData(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new NullReferenceException(nameof(mediator));
        _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
    }

    [HttpGet("/User/GetAllRegisteredUsersData")]
    [SwaggerOperation(
        Summary = "Получение данных пользователей",
        Description = "Для пагинации требуется вести в строку номер страницы, строка фильтра может быть пустой",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<UsersVm>>> HandleAsync(GetAllUsersCommand request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<UsersVm>>(result));
    }
}