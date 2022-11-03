using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Requests.Queries;
using ServicesContracts.Identity.Responses;
using User.Application.Contracts;

namespace User.Application.Features.Users.Queries.GetUsersData;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersCommand, Result<UsersVm>>
{
    private readonly IUserRepository _userRepository;
    public GetAllUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UsersVm>> Handle(GetAllUsersCommand request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetFilteredBatchOfData(request.PageSize, request.PageNumber, request.FilterString);
        if (!result.Any())
        {
            return Result.Error("Request list is empty");
        }

        var response = new UsersVm()
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            Users = result
        };
        return Result.Success(response);
    }
}