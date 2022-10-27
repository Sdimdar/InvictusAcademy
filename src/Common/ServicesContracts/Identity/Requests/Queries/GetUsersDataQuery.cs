using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Responses;

namespace ServicesContracts.Identity.Requests.Queries;

public class GetUsersDataQuery : IRequest<Result<UsersVm>>
{
    [FromQuery] public int Page { get; set; }
    [FromQuery] public int PageSize { get; set; }
    [FromQuery] public string? FilterString { get; set; }
}