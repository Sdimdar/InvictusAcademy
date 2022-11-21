using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.Courses.Requests.Modules.Queries;

namespace Courses.Application.Features.Modules.Queries.GetByIdQuery;

public class GetByIdQueryHandler : IRequestHandler<GetModuleByIdQuery, Result<ModuleInfoDbModel?>>
{
    private readonly IModuleInfoRepository _repository;
    private readonly IValidator<GetModuleByIdQuery> _validator;
    private readonly ILogger<GetByIdQueryHandler> _logger;

    public GetByIdQueryHandler(IValidator<GetModuleByIdQuery> validator,
                               IModuleInfoRepository repository, ILogger<GetByIdQueryHandler> logger)
    {
        _validator = validator;
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<ModuleInfoDbModel?>> Handle(GetModuleByIdQuery request,
                                                  CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validatorResult.IsValid)
        {
            return Result.Invalid(validatorResult.AsErrors());
        }
        try
        {
            return Result.Success(await _repository.GetAsync(request.Id, cancellationToken));
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError($"{BussinesErrors.InvalidOperationException.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.InvalidOperationException.ToString()}: {ex.Message}");
        }
    }
}
