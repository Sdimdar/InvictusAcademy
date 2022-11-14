using Ardalis.Result;
using MediatR;
using ServicesContracts.Identity.Requests.Queries;
using User.Application.Contracts;

namespace User.Application.Features.Users.Queries.GetUsersCount;

public class GetUsersCountQueryHandler : IRequestHandler<GetUsersCountQuery, Result<int>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersCountQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<int>> Handle(GetUsersCountQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetCountAsync();
    }
}