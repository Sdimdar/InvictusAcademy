using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using FluentValidation;
using MediatR;
using ServicesContracts.Identity.Requests.Queries;
using ServicesContracts.Identity.Responses;
using User.Application.Contracts;

namespace User.Application.Features.Users.Queries.GetUsersData;

public class GetUsersDataQueryHandler : IRequestHandler<GetUsersDataQuery, Result<UsersVm>>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<GetUsersDataQuery> _validator;
    private readonly IMapper _mapper;

    public GetUsersDataQueryHandler(IUserRepository userRepository, IMapper mapper, IValidator<GetUsersDataQuery> validator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<UsersVm>> Handle(GetUsersDataQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return Result.Invalid(validationResult.AsErrors());

        var usersCount = _userRepository.GetUsersCountAsync();
        if (request.PageSize == 0)
        {
            request.Page = 1;
            request.PageSize = await usersCount;
        }

        if (request.PageSize == 0) return Result.Error("Could not get users from the server");
        
        var data = await _userRepository.GetPaginatedAll(request.FilterString, request.PageSize, request.Page);
        UsersVm model = new()
        {
            Users = _mapper.Map<List<UserVm>>(data),
            Filter = request.FilterString,
            PageVm = new PageVm(await usersCount, request.Page, request.PageSize)
        };
        return Result.Success(model);
    }
}