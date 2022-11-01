using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Modules.Queries;

namespace Courses.Application.Features.Modules.Queries.GetModulesByListOfId;

public class GetModulesByListOfIdQueryHandler : IRequestHandler<GetModulesByListOfIdQuery, Result<List<ModuleInfoDbModel>?>>
{
    private readonly IModuleInfoRepository _repository;
    private readonly IValidator<GetModulesByListOfIdQuery> _validator;

    public GetModulesByListOfIdQueryHandler(IModuleInfoRepository repository,
                                            IValidator<GetModulesByListOfIdQuery> validator)
    {
        _repository = repository;
        _validator = validator;
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
            return Result.Error(ex.Message);
        }
    }
}
