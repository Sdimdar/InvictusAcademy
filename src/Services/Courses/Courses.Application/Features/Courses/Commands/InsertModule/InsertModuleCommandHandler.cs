using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Courses.Commands;

namespace Courses.Application.Features.Courses.Commands.InsertModule;

public class InsertModuleCommandHandler : IRequestHandler<InsertModuleCommand, Result<CourseInfoDbModel>>
{
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly IValidator<InsertModuleCommand> _validator;

    public InsertModuleCommandHandler(ICourseInfoRepository courseInfoRepository,
                                      IValidator<InsertModuleCommand> validator)
    {
        _courseInfoRepository = courseInfoRepository;
        _validator = validator;
    }

    public async Task<Result<CourseInfoDbModel>> Handle(InsertModuleCommand request, CancellationToken cancellationToken)
    {
        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return Result.Invalid(validateResult.AsErrors());
        }

        try
        {
            var result = await _courseInfoRepository.InsertModuleAsync(request.CourseId, request.ModuleId, request.Index, cancellationToken);
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
