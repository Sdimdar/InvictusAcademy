using Ardalis.Result;
using AutoMapper;
using MediatR;
using ServicesContracts.Identity.Requests.Queries;
using ServicesContracts.Identity.Responses;
using User.Application.Contracts;
using User.Domain.Entities;

namespace User.Application.Features.Users.Queries.GetUserData;

public class GetUserDataQueryHandler : IRequestHandler<GetUserDataQuery, Result<UserVm>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserDataQueryHandler(IUserRepository userRepository,
                                    IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserVm>> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
    {
        UserDbModel? user = await _userRepository.GetFirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null) return Result.Error("User with this Email not found");
        UserVm vm = _mapper.Map<UserVm>(user);
        return Result.Success(vm);
    }
}
