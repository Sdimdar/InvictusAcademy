using Ardalis.Result;
using AutoMapper;
using MediatR;
using ServicesContracts.Identity.Requests.Queries;
using ServicesContracts.Identity.Responses;
using User.Application.Contracts;

namespace User.Application.Features.Users.Queries.GetUsersListById;

public class GetUsersListByIdHandler : IRequestHandler<GetUsersEmailsByListIdQuery, Result<List<UsersEmailsByListIdVm>>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    public GetUsersListByIdHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<Result<List<UsersEmailsByListIdVm>>> Handle(GetUsersEmailsByListIdQuery request, CancellationToken cancellationToken)
    {
        if (!request.ListId.Any()) return Result.Error("Request list is empty");
        try
        {
            List<UsersEmailsByListIdVm> list = new();
            list = _mapper.Map<List<UsersEmailsByListIdVm>>(await _userRepository.GetUsersByIdList(request.ListId));
            if (!list.Any())
                return Result.Error("Response list is empty");
            return Result.Success(list);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}