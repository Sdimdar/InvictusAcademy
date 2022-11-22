using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.Courses.Requests.Courses.Commands;

namespace Courses.Application.Features.Courses.Commands.Create;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result<CourseDbModel>>
{
    private readonly ICourseRepository _coursesRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCourseCommand> _validator;
    private readonly ILogger<CreateCourseCommandHandler> _logger;

    public CreateCourseCommandHandler(ICourseRepository coursesRepository,
                                      IMapper mapper,
                                      IValidator<CreateCourseCommand> validator, ILogger<CreateCourseCommandHandler> logger)
    {
        _coursesRepository = coursesRepository;
        _mapper = mapper;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<CourseDbModel>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }

            var entity = _mapper.Map<CourseDbModel>(request);
            var result = await _coursesRepository.AddAsync(entity);
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
        }
    }
}
