using Ardalis.Result;
using CommonStructures;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.Courses.Requests.Modules.Queries;

namespace Courses.Application.Features.Modules.Queries.GetAll;

public class GetAllModulesQuerryHanler : IRequestHandler<GetAllModulesQuerry, Result<List<ModuleInfoDbModel>>>
{
    private readonly IModuleInfoRepository _repository;
    private readonly ILogger<GetAllModulesQuerryHanler> _logger;

    public GetAllModulesQuerryHanler(IModuleInfoRepository repository, ILogger<GetAllModulesQuerryHanler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<List<ModuleInfoDbModel>>> Handle(GetAllModulesQuerry request,
                                                        CancellationToken cancellationToken)
    {
        try
        {
            return Result.Success(await _repository.GetAsync(cancellationToken));
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError($"{BussinesErrors.InvalidOperationException.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.InvalidOperationException.ToString()}: {ex.Message}");
        }
    }
}
