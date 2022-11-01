using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Courses.Application.Contracts;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Modules.Commands;

namespace Courses.Application.Features.Modules.Commands.DeleteModule;

public class DeleteModuleCommandHanler : IRequestHandler<DeleteModuleCommand, Result>
{
    private readonly IModuleInfoRepository _repository;
    private readonly IValidator<DeleteModuleCommand> _validator;

    public DeleteModuleCommandHanler(IModuleInfoRepository repository,
                                      IValidator<DeleteModuleCommand> validator)
    {
        _repository = repository;
        _validator = validator;
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
            return Result.Error(ex.Message);
        }
    }
}
