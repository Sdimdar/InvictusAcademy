﻿using Ardalis.Result;
using Identity.Application.Features.Users.Queries.GetUserData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Application.Features.Users.Queries.GetUsersData;

public class GetUsersDataQuerry : IRequest<Result<UsersDataVm>>
{
    [FromQuery]public int Page { get; set; }
    [FromQuery]public string? FilterString { get; set; }
}