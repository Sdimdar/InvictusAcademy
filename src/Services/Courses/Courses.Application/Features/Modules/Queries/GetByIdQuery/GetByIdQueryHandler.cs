using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Modules.Queries;

namespace Courses.Application.Features.Modules.Queries.GetByIdQuery;

public class GetByIdQueryHandler : IRequestHandler<GetModuleByIdQuery, Result<ModuleInfoDbModel?>>
{
    private readonly IModuleInfoRepository _repository;
    private readonly IValidator<GetModuleByIdQuery> _validator;

    public GetByIdQueryHandler(IValidator<GetModuleByIdQuery> validator,
                               IModuleInfoRepository repository)
    {
        _validator = validator;
        _repository = repository;
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
            return Result.Error(ex.Message);
        }
    }
}
