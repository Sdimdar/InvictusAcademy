using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Responses;
using User.Application.Contracts;

namespace User.Application.Features.Users.Queries.GetUsersData;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersCommand, Result<GetAllRegisteredUsersVM>>
{
    private readonly IUserRepository _userRepository;
    public GetAllUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<GetAllRegisteredUsersVM>> Handle(GetAllUsersCommand request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetUsersByPage(request);
        if (!result.Any())
        {
            return Result.Error("Request list is empty");
        }

        var response = new GetAllRegisteredUsersVM()
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            Users = result
        };
        return Result.Success(response);
    }
}