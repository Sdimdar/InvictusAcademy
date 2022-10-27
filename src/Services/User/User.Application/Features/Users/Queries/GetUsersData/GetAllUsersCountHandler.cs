using Ardalis.Result;
using AutoMapper;
using MediatR;
using ServicesContracts.Identity.Requests.Querries;
using ServicesContracts.Identity.Responses;
using User.Application.Contracts;

namespace User.Application.Features.Users.Queries.GetUsersData;

public class GetUsersDataQuerryHandler : IRequestHandler<GetAllUsersCountQuery, Result<int>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersDataQuerryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(GetAllUsersCountQuery request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetUsersCount();
        return Result.Success(result);
    }
}