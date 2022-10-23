using Ardalis.Result;
using AutoMapper;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using MediatR;
using ServicesContracts.Courses.Requests.Commands;

namespace Courses.Application.Features.Courses.Commands.Create;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result<string>>
{
    private readonly ICourseInfosRepository _coursesRepository;
    private readonly IMapper _mapper;

    public CreateCourseCommandHandler(ICourseInfosRepository coursesRepository, IMapper mapper)
    {
        _coursesRepository = coursesRepository;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<CourseInfoDbModel>(request);
            await _coursesRepository.CreateAsync(entity);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
