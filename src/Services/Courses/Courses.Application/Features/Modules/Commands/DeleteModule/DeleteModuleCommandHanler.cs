using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using Courses.Application.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.Courses.Requests.Modules.Commands;

namespace Courses.Application.Features.Modules.Commands.DeleteModule;

public class DeleteModuleCommandHanler : IRequestHandler<DeleteModuleCommand, Result>
{
    private readonly IModuleInfoRepository _repository;
    private readonly IValidator<DeleteModuleCommand> _validator;
    private readonly ILogger<DeleteModuleCommandHanler> _logger;

    public DeleteModuleCommandHanler(IModuleInfoRepository repository,
                                      IValidator<DeleteModuleCommand> validator,
                                      ILogger<DeleteModuleCommandHanler> logger)
    {
        _repository = repository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteModuleCommand request,
                                     CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validatorResult.IsValid)
        {
            return Result.Invalid(validatorResult.AsErrors());
        }
        try
        {
            await _repository.RemoveAsync(request.Id, cancellationToken);
            return Result.Success();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError($"{BussinesErrors.InvalidOperationException.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.InvalidOperationException.ToString()}: {ex.Message}");
        }
    }
}
