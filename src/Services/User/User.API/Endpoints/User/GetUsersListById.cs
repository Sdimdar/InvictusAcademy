using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Requests.Queries;
using ServicesContracts.Identity.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace User.API.Endpoints.User;

public class GetUsersListById : EndpointBaseAsync
    .WithRequest<GetUsersEmailsByListIdQuery>
    .WithActionResult<DefaultResponseObject<List<UsersEmailsByListIdVm>>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetUsersListById(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/User/GetUsersById")]
    [SwaggerOperation(
        Summary = "Получение списка юзеров",
        Description = "Необходимо передать в теле запроса list id юзеров",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<List<UsersEmailsByListIdVm>>>> HandleAsync([FromBody] GetUsersEmailsByListIdQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<List<UsersEmailsByListIdVm>>>(result));
    }
}