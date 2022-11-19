using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.Courses.Requests.Modules.Commands;

namespace Courses.Application.Features.Modules.Commands.UpdateModule;

public class UpdateModuleCommandHanler : IRequestHandler<UpdateModuleCommand, Result<ModuleInfoDbModel>>
{
    private readonly IModuleInfoRepository _repository;
    private readonly IValidator<UpdateModuleCommand> _validator;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateModuleCommandHanler> _logger;

    public UpdateModuleCommandHanler(IModuleInfoRepository repository,
                                      IValidator<UpdateModuleCommand> validator,
                                      IMapper mapper, 
                                      ILogger<UpdateModuleCommandHanler> logger)
    {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<ModuleInfoDbModel>> Handle(UpdateModuleCommand request,
                                                        CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validatorResult.IsValid)
        {
            return Result.Invalid(validatorResult.AsErrors());
        }
        try
        {
            return Result.Success(await _repository.UpdateAsync(request.Id, _mapper.Map<ModuleInfoDbModel>(request), cancellationToken));
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError($"{BussinesErrors.InvalidOperationException.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.InvalidOperationException.ToString()}: {ex.Message}");
        }
    }
}
