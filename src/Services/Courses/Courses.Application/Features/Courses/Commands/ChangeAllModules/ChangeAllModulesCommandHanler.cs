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

namespace Courses.Application.Features.Courses.Commands.ChangeAllModules;

public class ChangeAllModulesCommandHanler : IRequestHandler<ChangeAllModulesCommand, Result<CourseInfoVm>>
{
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly IModuleInfoRepository _moduleInfoRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IValidator<ChangeAllModulesCommand> _validator;
    private readonly IMapper _mapper;
    private readonly ILogger<ChangeAllModulesCommandHanler> _logger;


    public ChangeAllModulesCommandHanler(ICourseInfoRepository courseInfoRepository,
                                         IValidator<ChangeAllModulesCommand> validator,
                                         ICourseRepository courseRepository,
                                         IMapper mapper,
                                         IModuleInfoRepository moduleInfoRepository, ILogger<ChangeAllModulesCommandHanler> logger)
    {
        _courseInfoRepository = courseInfoRepository;
        _validator = validator;
        _courseRepository = courseRepository;
        _mapper = mapper;
        _moduleInfoRepository = moduleInfoRepository;
        _logger = logger;
    }

    public async Task<Result<CourseInfoVm>> Handle(ChangeAllModulesCommand request,
                                                   CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        try
        {
            var courseData = await _courseRepository.GetByIdAsync(request.CourseId);
            if (courseData is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Course with Id: {request.CourseId} not found");
                return Result.Error($"{BussinesErrors.NotFound.ToString()}: Course with Id: {request.CourseId} not found");
            }
            UniqueList<int> moduleIds = await _moduleInfoRepository.CheckModulesOnExist(request.ModulesId, cancellationToken);
            var courseInfo = await _courseInfoRepository.GetAsync(request.CourseId, cancellationToken);
            courseInfo!.SetModules(moduleIds);
            courseInfo = await _courseInfoRepository.UpdateAsync(request.CourseId, courseInfo, cancellationToken);
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
            return Result.Error(ex.Message);
        }
    }
}
