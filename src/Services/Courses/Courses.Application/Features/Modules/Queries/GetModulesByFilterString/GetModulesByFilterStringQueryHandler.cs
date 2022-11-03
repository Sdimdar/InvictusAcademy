using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Modules.Queries;

namespace Courses.Application.Features.Modules.Queries.GetModulesByFilterString;

public class GetModulesByFilterStringQueryHandler : IRequestHandler<GetModulesByFilterStringQuery, Result<List<ModuleInfoDbModel>?>>
{
    private readonly IValidator<GetModulesByFilterStringQuery> _validator;
    private readonly IModuleInfoRepository _repository;

    public GetModulesByFilterStringQueryHandler(IValidator<GetModulesByFilterStringQuery> validator,
                                                IModuleInfoRepository repository)
    {
        _validator = validator;
        _repository = repository;
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
            return Result.Error(ex.Message);
        }
    }
}