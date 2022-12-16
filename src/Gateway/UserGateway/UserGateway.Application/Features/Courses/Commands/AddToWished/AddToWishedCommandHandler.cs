using Ardalis.Result;
using Courses.Domain.Entities;
using MediatR;
using ServicesContracts.Courses.Requests.Courses.Commands;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.Courses.Commands.AddToWished;

public class AddToWishedCommandHandler : IRequestHandler<AddToWishedCourseCommand, Result>
{
    private readonly ICoursesService _coursesService;
    private readonly IUserService _userService;

    public AddToWishedCommandHandler(ICoursesService coursesService, IUserService userService)
    {
        _coursesService = coursesService;
        _userService = userService;
    }

    public async Task<Result> Handle(AddToWishedCourseCommand request, CancellationToken cancellationToken)
    {
        var response = await _coursesService.AddToWishedCourse(request, cancellationToken);
        if (response.IsSuccess) return Result.Success();
        if (response.Errors.Count() != 0) return Result.Error(response.Errors);
        return Result.Invalid(response.ValidationErrors.ToList());
    }
}