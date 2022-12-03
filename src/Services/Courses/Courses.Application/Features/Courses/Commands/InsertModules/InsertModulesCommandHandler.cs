using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using Courses.Application.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Features.Courses.Commands.InsertModules;

public class InsertModulesCommandHandler : IRequestHandler<InsertModulesCommand, Result<CourseInfoVm>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly IModuleInfoRepository _moduleInfoRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<InsertModulesCommand> _validator;
    private readonly ILogger<InsertModulesCommandHandler> _logger;

    public InsertModulesCommandHandler(IValidator<InsertModulesCommand> validate,
                                       ICourseInfoRepository courseInfoRepository,
                                       ICourseRepository courseRepository,
                                       IModuleInfoRepository moduleInfoRepository,
                                       IMapper mapper, ILogger<InsertModulesCommandHandler> logger)
    {
        _validator = validate;
        _courseInfoRepository = courseInfoRepository;
        _courseRepository = courseRepository;
        _moduleInfoRepository = moduleInfoRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<CourseInfoVm>> Handle(InsertModulesCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            return Result.Invalid(validatorResult.AsErrors());
        }

        try
        {
            var courseData = await _courseRepository.GetByIdAsync(request.CourseId);
            if (courseData is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Course with Id: {request.CourseId} not found");
                return Result.Error($"{BussinesErrors.NotFound.ToString()}: Course with Id: {request.CourseId} not found");
            }
            UniqueList<int> modulesId = await _moduleInfoRepository.CheckModulesOnExist(request.ModulesId, cancellationToken);
            if (modulesId is null)
            {
                _logger.LogWarning($"{BussinesErrors.DataIsNotExist.ToString()}: All modules is not exist");
                return Result.Error($"{BussinesErrors.DataIsNotExist.ToString()}: All modules is not exist");
            }
            var courseInfo = await _courseInfoRepository.GetAsync(request.CourseId, cancellationToken);
            courseInfo!.TryInsertModules(modulesId, request.StartIndex);
            await _courseInfoRepository.UpdateAsync(request.CourseId, courseInfo, cancellationToken);
            CourseInfoVm vm = new()
            {
                CourseData = _mapper.Map<CourseVm>(courseData),
                ModulesId = courseInfo.ModulesId
            };
            return Result.Success(vm);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError($"{BussinesErrors.InvalidOperationException.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.InvalidOperationException.ToString()}: {ex.Message}");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            _logger.LogError($"{BussinesErrors.ArgumentOutOfRangeException.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.ArgumentOutOfRangeException.ToString()}: {ex.Message}");
        }
    }
}
