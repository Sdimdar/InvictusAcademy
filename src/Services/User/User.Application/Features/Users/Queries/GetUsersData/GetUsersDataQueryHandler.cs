using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.Identity.Requests.Queries;
using ServicesContracts.Identity.Responses;
using User.Application.Contracts;

namespace User.Application.Features.Users.Queries.GetUsersData;

public class GetUsersDataQueryHandler : IRequestHandler<GetUsersDataQuery, Result<UsersVm>>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<GetUsersDataQuery> _validator;
    private readonly ILogger<GetUsersDataQueryHandler> _logger;

    public GetUsersDataQueryHandler(IUserRepository userRepository, IValidator<GetUsersDataQuery> validator, ILogger<GetUsersDataQueryHandler> logger)
    {
        _userRepository = userRepository;
        _validator = validator;
        _logger = logger;
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

        if (await usersCount == 0)
        {
            _logger.LogWarning($"{BussinesErrors.ListIsEmpty.ToString()}: Users list is empty");
            return Result.Error($"{BussinesErrors.ListIsEmpty.ToString()}: Users list is empty");
        }

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