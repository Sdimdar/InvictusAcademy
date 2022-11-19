using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.Courses.Requests.Modules.Queries;

namespace Courses.Application.Features.Modules.Queries.GetModulesByListOfId;

public class GetModulesByListOfIdQueryHandler : IRequestHandler<GetModulesByListOfIdQuery, Result<List<ModuleInfoDbModel>?>>
{
    private readonly IModuleInfoRepository _repository;
    private readonly IValidator<GetModulesByListOfIdQuery> _validator;
    private readonly ILogger<GetModulesByListOfIdQueryHandler> _logger;
    public GetModulesByListOfIdQueryHandler(IModuleInfoRepository repository,
                                            IValidator<GetModulesByListOfIdQuery> validator, ILogger<GetModulesByListOfIdQueryHandler> logger)
    {
        _repository = repository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<List<ModuleInfoDbModel>?>> Handle(GetModulesByListOfIdQuery request,
                                                        CancellationToken cancellationToken)
    {
        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return Result.Invalid(validateResult.AsErrors());
        }
        try
        {
            return Result.Success(await _repository.GetModulesByListOfIdAsync(request.ModulesId, cancellationToken));
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError($"{BussinesErrors.InvalidOperationException.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.InvalidOperationException.ToString()}: {ex.Message}");
        }
    }
}
