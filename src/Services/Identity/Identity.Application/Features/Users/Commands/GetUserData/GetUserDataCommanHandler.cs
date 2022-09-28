using Ardalis.Result;
using AutoMapper;
using Identity.Application.Contracts;
using Identity.Domain.Entities;
using MediatR;

namespace Identity.Application.Features.Users.Commands.GetUserData;

public class GetUserDataCommanHandler : IRequestHandler<GetUserDataCommand, Result<UserDataVm>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserDataCommanHandler(IUserRepository userRepository,
                                    IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserDataVm>> Handle(GetUserDataCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null) return Result.NotFound("User with this Email not found");
        UserDataVm vm = _mapper.Map<UserDataVm>(user);
        return Result.Success(vm);
    }
}
