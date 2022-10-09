using Ardalis.Result;
using AutoMapper;
using Identity.Application.Contracts;
using Identity.Domain.Entities;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetUserData;

public class GetUserDataQuerryHandler : IRequestHandler<GetUserDataQuerry, Result<UserDataVm>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserDataQuerryHandler(IUserRepository userRepository,
                                    IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserDataVm>> Handle(GetUserDataQuerry request, CancellationToken cancellationToken)
    {
        UserDbModel? user = await _userRepository.GetFirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null) return Result.NotFound("User with this Email not found");
        UserDataVm vm = _mapper.Map<UserDataVm>(user);
        return Result.Success(vm);
    }
}
