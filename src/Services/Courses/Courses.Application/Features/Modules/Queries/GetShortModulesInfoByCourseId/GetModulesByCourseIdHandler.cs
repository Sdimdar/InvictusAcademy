using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Courses.Application.Features.Modules.Queries.GetShortModulesInfoByCourseId;

public class GetModulesByCourseIdHandler:
    IRequestHandler<ServicesContracts.Courses.Requests.Modules.Queries.GetModulesByCourseId, Result<List<ModuleInfoDbModel>?>>
{
    private readonly IModuleInfoRepository _repository;
    private readonly IValidator<ServicesContracts.Courses.Requests.Modules.Queries.GetModulesByCourseId> _validator;
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly ILogger<GetModulesByCourseIdHandler> _logger;


    public GetModulesByCourseIdHandler(IModuleInfoRepository repository, IValidator<ServicesContracts.Courses.Requests.Modules.Queries.GetModulesByCourseId> validator, ICourseInfoRepository courseInfoRepository, ILogger<GetModulesByCourseIdHandler> logger)
    {
        _repository = repository;
        _validator = validator;
        _courseInfoRepository = courseInfoRepository;
        _logger = logger;
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
            if (courseInfo is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Course with Id: {request.CourseId} not found");
                return Result.Error($"{BussinesErrors.NotFound.ToString()}: Course with Id: {request.CourseId} not found");
            }

            if (!courseInfo.ModulesId.Any())
            {
                _logger.LogWarning($"{BussinesErrors.ListIsEmpty.ToString()}: Course with Id: {request.CourseId} havent any modules");
                return Result.Error($"{BussinesErrors.ListIsEmpty.ToString()}: Course with Id: {request.CourseId} havent any modules");
            }
            return Result.Success(await _repository.GetModulesByListOfIdAsync(courseInfo.ModulesId, cancellationToken));
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError($"{BussinesErrors.InvalidOperationException.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.InvalidOperationException.ToString()}: {ex.Message}");
        }
    }
}