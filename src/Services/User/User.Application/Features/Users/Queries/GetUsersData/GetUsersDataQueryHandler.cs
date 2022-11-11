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

    public GetUsersDataQueryHandler(IUserRepository userRepository, IValidator<GetUsersDataQuery> validator)
    {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<Result<UsersVm>> Handle(GetUsersDataQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return Result.Invalid(validationResult.AsErrors());

        var usersCount = _userRepository.GetCountAsync();
        if (request.PageSize == 0)
        {
            request.PageNumber = 1;
            request.PageSize = await usersCount;
        }

        if (await usersCount == 0) return Result.Error("Users list is empty");

        var command = new GetAllUsersCommand()
        {
            PageSize = request.PageSize,
            PageNumber = request.PageNumber,
            FilterString = request.FilterString
        };
        
        var data = await _userRepository.GetFilteredBatchOfData(request.PageSize, request.PageNumber, request.FilterString);
        UsersVm model = new()
        {
            Users = data,
            Filter = request.FilterString,
            PageVm = new PageVm(await usersCount, request.PageNumber, request.PageSize)
        };
        return Result.Success(model);
    }
}