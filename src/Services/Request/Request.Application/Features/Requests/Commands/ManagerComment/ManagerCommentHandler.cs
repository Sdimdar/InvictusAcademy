using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Request.Application.Contracts;
using ServicesContracts.Request.Requests.Commands;

namespace Request.Application.Features.Requests.Commands.ManagerComment;

public class ManagerCommentHandler : IRequestHandler<ManagerCommentCommand, Result<string>>
{
    private readonly IRequestRepository _requestRepository;
    private readonly IValidator<ManagerCommentCommand> _validator;
    private readonly ILogger<ManagerCommentHandler> _logger;

    public ManagerCommentHandler(IRequestRepository requestRepository, IValidator<ManagerCommentCommand> validator, ILogger<ManagerCommentHandler> logger)
    {
        _requestRepository = requestRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(ManagerCommentCommand request, CancellationToken cancellationToken)
    {
        var result = await _requestRepository.GetFirstOrDefaultAsync(r => r.Id == request.Id);
        if (result == null)
        {
            _logger.LogWarning($"{BussinesErrors.DataIsNotExist.ToString()}: Data with this id does not exist");
            return Result.Error($"{BussinesErrors.DataIsNotExist.ToString()}: Data with this id does not exist");
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        result.ManagerComment = request.ManagerComment;
        await _requestRepository.UpdateAsync(result);
        return Result.Success();
    }
}