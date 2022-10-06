using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Application.Features.Users.Queries.GetUsersData;

public class GetUsersDataQuerry : IRequest<Result<UsersDataVm>>
{
    [FromQuery]public int Page { get; set; }
    [FromQuery]public int PageSize { get; set; }
    [FromQuery]public string? FilterString { get; set; }
}