using Ardalis.Result;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using MediatR;
using ServicesContracts.Courses.Requests.Modules.Queries;

namespace Courses.Application.Features.Modules.Queries.GetAll;

public class GetAllModulesQuerryHanler : IRequestHandler<GetAllModulesQuerry, Result<List<ModuleInfoDbModel>>>
{
    private readonly IModuleInfoRepository _repository;

    public GetAllModulesQuerryHanler(IModuleInfoRepository repository)
    {
        _repository = repository;
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
            return Result.Error(ex.Message);
        }
    }
}
