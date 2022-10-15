using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Responses;

namespace Identity.Application.Features.Users.Queries.GetUsersData;

public class GetUsersDataQuerry : IRequest<Result<UsersVm>>
{
    [FromQuery]public int Page { get; set; }
    [FromQuery]public int PageSize { get; set; }
    [FromQuery]public string? FilterString { get; set; }
}