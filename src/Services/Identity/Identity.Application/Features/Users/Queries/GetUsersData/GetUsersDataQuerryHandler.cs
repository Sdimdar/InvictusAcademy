using Ardalis.Result;
using Identity.Application.Contracts;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetUsersData;

public class GetUsersDataQuerryHandler : IRequestHandler<GetUsersDataQuerry, Result<UsersDataVm>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersDataQuerryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UsersDataVm>> Handle(GetUsersDataQuerry request, CancellationToken cancellationToken)
    {
        int pageSize = 1;
        var model = await _userRepository.GetPaginatedAll(pageSize, request.Page);
        if (model is null)
            return Result.Error("Сould not get users from the server");
        return Result.Success(model);
    }
}