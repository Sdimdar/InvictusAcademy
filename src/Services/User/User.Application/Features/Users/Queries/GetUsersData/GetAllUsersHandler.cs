using Ardalis.Result;
using AutoMapper;
using MediatR;
using ServicesContracts.Identity.Requests.Queries;
using ServicesContracts.Identity.Responses;
using User.Application.Contracts;

namespace User.Application.Features.Users.Queries.GetUsersData;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersCommand, Result<UsersVm>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public GetAllUsersHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UsersVm>> Handle(GetAllUsersCommand request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetFilteredBatchOfData(request.PageSize, request.PageNumber, request.FilterString);
        if (!result.Any())
        {
            return Result.Error("Request list is empty");
        }

        var users = _mapper.Map<List<UserVm>>(result);
        var response = new UsersVm()
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            Users = users
        };
        return Result.Success(response);
    }
}