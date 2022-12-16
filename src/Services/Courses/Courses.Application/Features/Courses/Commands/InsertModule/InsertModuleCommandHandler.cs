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

namespace Courses.Application.Features.Courses.Commands.InsertModule;

public class InsertModuleCommandHandler : IRequestHandler<InsertModuleCommand, Result<CourseInfoVm>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly IModuleInfoRepository _moduleInfoRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<InsertModuleCommand> _validator;
    private readonly ILogger<InsertModuleCommandHandler> _logger;

    public InsertModuleCommandHandler(ICourseInfoRepository courseInfoRepository,
                                      IValidator<InsertModuleCommand> validator,
                                      IModuleInfoRepository moduleInfoRepository,
                                      IMapper mapper,
                                      ICourseRepository courseRepository, ILogger<InsertModuleCommandHandler> logger)
    {
        _courseInfoRepository = courseInfoRepository;
        _validator = validator;
        _moduleInfoRepository = moduleInfoRepository;
        _mapper = mapper;
        _courseRepository = courseRepository;
        _logger = logger;
    }

    public async Task<Result<CourseInfoVm>> Handle(InsertModuleCommand request, CancellationToken cancellationToken)
    {
        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return Result.Invalid(validateResult.AsErrors());
        }

        try
        {
            var courseData = await _courseRepository.GetByIdAsync(request.CourseId);
            if (courseData is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Course with Id: {request.CourseId} not found");
                return Result.Error($"{BussinesErrors.NotFound.ToString()}: Course with Id: {request.CourseId} not found");
            }

            if (await _moduleInfoRepository.GetAsync(request.ModuleId, cancellationToken) is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Module with id {request.ModuleId} not found");
                return Result.Error($"{BussinesErrors.NotFound.ToString()}: Module with id {request.ModuleId} not found");
            }
            var courseInfo = await _courseInfoRepository.GetAsync(request.CourseId, cancellationToken);
            courseInfo!.TryInsertModule(request.ModuleId, request.Index);
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
            _logger.LogError($"{BussinesErrors.ArgumentOutOfRangeException.ToString()}: {ex.Message}"); ;
            return Result.Error($"{BussinesErrors.ArgumentOutOfRangeException.ToString()}: {ex.Message}"); ;
        }
    }
}
