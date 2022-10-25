using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Requests.Queries;

namespace User.API.Endpoints.User;

public class GetUsersCount : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<DefaultResponseObject<int>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetUsersCount(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/User/Count")]
    public override async Task<ActionResult<DefaultResponseObject<int>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(new GetUsersCountQuery(), cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<int>>(result));
    }
}