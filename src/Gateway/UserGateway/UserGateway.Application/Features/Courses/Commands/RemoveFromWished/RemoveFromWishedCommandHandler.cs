using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Requests.Courses.Commands;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.Courses.Commands.RemoveFromWished;

public class RemoveFromWishedCommandHandler : IRequestHandler<RemoveFromWishedCommand, Result>
{
    private readonly ICoursesService _coursesService;

    public RemoveFromWishedCommandHandler(ICoursesService coursesService)
    {
        _coursesService = coursesService;
    }

    public async Task<Result> Handle(RemoveFromWishedCommand request, CancellationToken cancellationToken)
    {
        var response = await _coursesService.RemoveFromWishedCourse(request, cancellationToken);
        if (response.IsSuccess) return Result.Success();
        if (response.Errors.Count() != 0) return Result.Error(response.Errors);
        return Result.Invalid(response.ValidationErrors.ToList());
    }
}