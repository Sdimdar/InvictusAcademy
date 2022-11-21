using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.Courses.Requests.Modules.Queries;

namespace Courses.Application.Features.Modules.Queries.GetModulesByFilterString;

public class GetModulesByFilterStringQueryHandler : IRequestHandler<GetModulesByFilterStringQuery, Result<List<ModuleInfoDbModel>?>>
{
    private readonly IValidator<GetModulesByFilterStringQuery> _validator;
    private readonly IModuleInfoRepository _repository;
    private readonly ILogger<GetModulesByFilterStringQueryHandler> _logger;

    public GetModulesByFilterStringQueryHandler(IValidator<GetModulesByFilterStringQuery> validator,
                                                IModuleInfoRepository repository, ILogger<GetModulesByFilterStringQueryHandler> logger)
    {
        _validator = validator;
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<List<ModuleInfoDbModel>?>> Handle(GetModulesByFilterStringQuery request,
                                                        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        try
        {
            return Result.Success(await _repository.GetModulesByFilterStringAsync(request.FilterString, cancellationToken));
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError($"{BussinesErrors.InvalidOperationException.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.InvalidOperationException.ToString()}: {ex.Message}");
        }
    }
}