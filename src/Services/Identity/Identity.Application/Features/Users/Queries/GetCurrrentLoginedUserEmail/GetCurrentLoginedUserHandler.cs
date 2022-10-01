using Ardalis.Result;
using AutoMapper;
using Identity.Application.Contracts;
using Identity.Domain.Entities;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetCurrrentLoginedUserEmail;

public class GetCurrentLoginedUserHandler : IRequestHandler<GetCurrentLoginedUserEmailQuerry, Result<GetCurrentLoginedUserEmailVm>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetCurrentLoginedUserHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetCurrentLoginedUserEmailVm>> Handle(GetCurrentLoginedUserEmailQuerry request, CancellationToken cancellationToken)
    {
        string? userEmailfromClaim = request.User.Identity!.Name;
        if (userEmailfromClaim == null) return Result.Error("Current user error, don't have email");
        User? user = await _userRepository.GetByEmailAsync(userEmailfromClaim);
        if (user == null) return Result.NotFound("Error by find currnet logined user in Data base");
        return Result.Success(_mapper.Map<GetCurrentLoginedUserEmailVm>(user));
    }
}
