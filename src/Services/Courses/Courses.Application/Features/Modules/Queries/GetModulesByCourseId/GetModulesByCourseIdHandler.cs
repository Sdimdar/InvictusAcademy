using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Features.Modules.Queries.GetModulesByCourseId;

public class GetModulesByCourseIdHandler:
    IRequestHandler<ServicesContracts.Courses.Requests.Modules.Queries.GetModulesByCourseId, Result<List<ModuleInfoDbModel>?>>
{
    private readonly IModuleInfoRepository _repository;
    private readonly IValidator<ServicesContracts.Courses.Requests.Modules.Queries.GetModulesByCourseId> _validator;
    private readonly ICourseInfoRepository _courseInfoRepository;


    public GetModulesByCourseIdHandler(IModuleInfoRepository repository, IValidator<ServicesContracts.Courses.Requests.Modules.Queries.GetModulesByCourseId> validator, ICourseInfoRepository courseInfoRepository)
    {
        _repository = repository;
        _validator = validator;
        _courseInfoRepository = courseInfoRepository;
    }

    public async Task<Result<List<ModuleInfoDbModel>?>> Handle(ServicesContracts.Courses.Requests.Modules.Queries.GetModulesByCourseId request, CancellationToken cancellationToken)
    {
        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return Result.Invalid(validateResult.AsErrors());
        }
        try
        {
            var courseInfo = await _courseInfoRepository.GetAsync(request.CourseId, cancellationToken);
            if (courseInfo is null) return Result.Error($"Course with Id: {request.CourseId} not found");
            if(!courseInfo.ModulesId.Any()) return Result.Error($"Course with Id: {request.CourseId} havent any modules");
            return Result.Success(await _repository.GetModulesByListOfIdAsync(courseInfo.ModulesId, cancellationToken));
        }
        catch (InvalidOperationException ex)
        {
            return Result.Error(ex.Message);
        }
    }
}