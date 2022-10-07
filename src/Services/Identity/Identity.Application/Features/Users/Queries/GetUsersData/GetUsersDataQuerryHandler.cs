﻿using Ardalis.Result;
using AutoMapper;
using Identity.Application.Contracts;
using Identity.Application.Features.Users.Queries.GetUserData;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetUsersData;

public class GetUsersDataQuerryHandler : IRequestHandler<GetUsersDataQuerry, Result<UsersDataVm>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersDataQuerryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UsersDataVm>> Handle(GetUsersDataQuerry request, CancellationToken cancellationToken)
    {
        var data = await _userRepository.GetPaginatedAll(request.FilterString, request.PageSize, request.Page);
        if (data.Item1 is null)
            return Result.Error("Сould not get users from the server");
        UsersDataVm model = new()
        {
            Users = _mapper.Map<IEnumerable<UserDataVm>>(data.Item1),
            Filter = request.FilterString,
            PageVm = new PageVm(data.Item2, request.Page, request.PageSize)
        };
        return Result.Success(model);
    }
}