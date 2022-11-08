using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Modules.Queries;


namespace Courses.Application.Features.Modules.Queries.GetFullByCourseId;

public class GetFullByCourseIdHandler: IRequestHandler<GetFullByCourseIdQuery, Result<List<ModuleInfoDbModel>?>>
{
    private readonly IModuleInfoRepository _repository;
    private readonly IValidator<GetFullByCourseIdQuery> _validator;
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseInfoRepository _courseInfoRepository;

    public GetFullByCourseIdHandler(IModuleInfoRepository repository, IValidator<GetFullByCourseIdQuery> validator, ICourseRepository courseRepository, ICourseInfoRepository courseInfoRepository)
    {
        _repository = repository;
        _validator = validator;
        _courseRepository = courseRepository;
        _courseInfoRepository = courseInfoRepository;
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
            if(!courseIsPaid) return Result.Error("You must purchase the course to access.");
            var courseInfo = await _courseInfoRepository.GetAsync(request.CourseId, cancellationToken);
            if (courseInfo is null) return Result.Error($"Course with Id: {request.CourseId} not found");
            if(!courseInfo.ModulesId.Any()) return Result.Error($"Course with Id: {request.CourseId} have not any modules");
            return Result.Success(await _repository.GetModulesByListOfIdAsync(courseInfo.ModulesId, cancellationToken));
        }
        catch (InvalidOperationException ex)
        {
            return Result.Error(ex.Message);
        }
    }
}