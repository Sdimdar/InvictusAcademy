using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.Courses.Requests.Modules.Queries;


namespace Courses.Application.Features.Modules.Queries.GetFullByCourseId;

public class GetFullByCourseIdHandler: IRequestHandler<GetFullByCourseIdQuery, Result<List<ModuleInfoDbModel>?>>
{
    private readonly IModuleInfoRepository _repository;
    private readonly IValidator<GetFullByCourseIdQuery> _validator;
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly ILogger<GetFullByCourseIdHandler> _logger;

    public GetFullByCourseIdHandler(IModuleInfoRepository repository, IValidator<GetFullByCourseIdQuery> validator, ICourseRepository courseRepository, ICourseInfoRepository courseInfoRepository, ILogger<GetFullByCourseIdHandler> logger)
    {
        _repository = repository;
        _validator = validator;
        _courseRepository = courseRepository;
        _courseInfoRepository = courseInfoRepository;
        _logger = logger;
    }

    public async Task<Result<List<ModuleInfoDbModel>?>> Handle(GetFullByCourseIdQuery request, CancellationToken cancellationToken)
    {
        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return Result.Invalid(validateResult.AsErrors());
        }
        try
        {
            var courseIsPaid =await _courseRepository.CourseIsPaid(request.UserId, request.CourseId);
            if(!courseIsPaid) 
            {
                _logger.LogWarning($"{BussinesErrors.BoolIsNotTrue.ToString()}: You must purchase the course to access.");
                return Result.Error($"{BussinesErrors.BoolIsNotTrue.ToString()}: You must purchase the course to access.");
            }
            var courseInfo = await _courseInfoRepository.GetAsync(request.CourseId, cancellationToken);
            if (courseInfo is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Course with Id: {request.CourseId} not found");
                return Result.Error($"{BussinesErrors.NotFound.ToString()}: Course with Id: {request.CourseId} not found");
            }

            if (!courseInfo.ModulesId.Any())
            {
                _logger.LogWarning($"{BussinesErrors.ListIsEmpty.ToString()}: Course with Id: {request.CourseId} have not any modules");
                return Result.Error($"{BussinesErrors.ListIsEmpty.ToString()}: Course with Id: {request.CourseId} have not any modules");
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