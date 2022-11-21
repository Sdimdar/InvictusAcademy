using Ardalis.Result;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Request.Application.Contracts;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Request.Requests.Commands;
using User.Application.Contracts;

namespace User.Application.Features.Users.Commands.ChangeBanStatus;

public class ChangeBanStatusHandler: IRequestHandler<ToBanCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<EditCommand> _validator;
    private readonly ILogger<ChangeBanStatusHandler> _logger;

    public ChangeBanStatusHandler(IUserRepository userRepository, ILogger<ChangeBanStatusHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }
    
    public async Task<Result<string>> Handle(ToBanCommand request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetFirstOrDefaultAsync(r => r.Id == request.Id);
        if (result == null)
        {
            _logger.LogWarning($"{BussinesErrors.DataIsNotExist.ToString()}: Data with this id does not exist");
            return Result.Error($"{BussinesErrors.DataIsNotExist.ToString()}: Data with this id does not exist");
        }

        if (result.IsBanned)
        {
            result.IsBanned = false;
            await _userRepository.UpdateAsync(result);
            return Result.Success();
        }
        result.IsBanned = true;
        await _userRepository.UpdateAsync(result);
        return Result.Success();
            
    }
}