﻿using Ardalis.Result;
using AutoMapper;
using MediatR;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.User.Querries.GetUserData;

public class GetUserDataQuerryHandler : IRequestHandler<GetUserDataQuerry, Result<GetUserDataVm>>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetUserDataQuerryHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<Result<GetUserDataVm>> Handle(GetUserDataQuerry request, CancellationToken cancellationToken)
    {
        if (request.Email is null) return Result.Error("User not autorized");
        var response = await _userService.GetUserAsync(request.Email, cancellationToken);
        var data = _mapper.Map<GetUserDataVm>(response.Value);
        if (response.IsSuccess) return Result.Success(data);
        if (response.Errors.Count() != 0) return Result.Error(response.Errors);
        return Result.Invalid(response.ValidationErrors.ToList());
    }
}
