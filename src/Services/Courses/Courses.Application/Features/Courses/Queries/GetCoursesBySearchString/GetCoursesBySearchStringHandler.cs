using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using Courses.Domain.Entities.CourseInfo;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Features.Courses.Queries.GetCoursesBySearchString;

public class GetCoursesBySearchStringHandler : IRequestHandler<GetCoursesBySearchStringCommand, Result<CoursesVm>>
{
    private readonly IValidator<GetCoursesBySearchStringCommand> _validator;
    private readonly ICourseRepository _courseRepository;
    private readonly IModuleInfoRepository _moduleInfoRepository;
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly IMapper _mapper;

    public GetCoursesBySearchStringHandler(IValidator<GetCoursesBySearchStringCommand> validator,
        ICourseRepository courseRepository, IModuleInfoRepository moduleInfoRepository, IMapper mapper,
        ICourseInfoRepository courseInfoRepository)
    {
        _validator = validator;
        _courseRepository = courseRepository;
        _moduleInfoRepository = moduleInfoRepository;
        _mapper = mapper;
        _courseInfoRepository = courseInfoRepository;
    }

    public async Task<Result<CoursesVm>> Handle(GetCoursesBySearchStringCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        
        
        List<CourseDbModel> courses = await _courseRepository.GetCoursesByFilter(p =>
            p.Name.Contains(request.SearchString) || p.Description.Contains(request.SearchString));
        // TODO заставить работать
        //List<int> modules = (await _moduleInfoRepository.GetModulesByFilterStringAsync(request.SearchString, cancellationToken))!.Select(e => e.Id).ToList();
        //List<CourseDbModel> coursesByModulesSearch = await GetCoursesWithModulesAsync(modules, cancellationToken);
        //courses = courses.UnionBy(coursesByModulesSearch, c => c.Id).ToList();
        CoursesVm result = new() {Courses = _mapper.Map<List<CourseVm>>(courses)};
        return Result.Success(result);
    }

    private async Task<List<CourseDbModel>> GetCoursesWithModulesAsync(List<int> modulesIds, CancellationToken cancellationToken)
    {
        List<int> coursesIds = new();
        foreach (int id in modulesIds)
        {
            coursesIds = (await _courseInfoRepository.GetCoursesByModulesId(modulesIds, cancellationToken)).Select(p => p.Id).ToList();
        }

        return await _courseRepository.GetCoursesByIdList(coursesIds);
    }
}