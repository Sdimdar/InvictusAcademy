using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using Courses.Application.Contracts;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Features.Courses.Commands.RemoveModule;

public class RemoveModuleCommandHandler : IRequestHandler<RemoveModuleCommand, Result<CourseInfoVm>>
{
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<RemoveModuleCommand> _validator;

    public RemoveModuleCommandHandler(ICourseInfoRepository courseInfoRepository,
                                      IValidator<RemoveModuleCommand> validator,
                                      ICourseRepository courseRepository,
                                      IMapper mapper)
    {
        _courseInfoRepository = courseInfoRepository;
        _validator = validator;
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<Result<CourseInfoVm>> Handle(RemoveModuleCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validatorResult.IsValid)
        {
            return Result.Invalid(validatorResult.AsErrors());
        }

        try
        {
            var courseData = await _courseRepository.GetByIdAsync(request.CourseId);
            if (courseData is null) return Result.Error($"Course with Id: {request.CourseId} not found");
            var courseInfo = await _courseInfoRepository.GetAsync(request.CourseId, cancellationToken);
            if (courseInfo is null) return Result.Error($"Course with Id: {request.CourseId} not found");
            courseInfo.DeleteModule(request.ModuleId);
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
            return Result.Error(ex.Message);
        }
    }
}
