using Ardalis.Result;
using DataTransferLib.Models;
using MediatR;
using ServicesContracts.Courses.Requests.Tests.Commands;
using ServicesContracts.Courses.Responses;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.Courses.Commands.CheckTestAnswers;

public class CheckTestAnswersCommandHandler : IRequestHandler<CheckTestAnswersCommand, Result<TestResultVm>>
{
    private ICoursesService _coursesService;

    public CheckTestAnswersCommandHandler(ICoursesService coursesService)
    {
        _coursesService = coursesService;
    }

    public async Task<Result<TestResultVm>> Handle(CheckTestAnswersCommand request, CancellationToken cancellationToken)
    {
        DefaultResponseObject<TestResultVm> result = await _coursesService.CheckTestAnswer(request, cancellationToken);
        if (!result.IsSuccess)
        {
            if (result.Errors?.Length != 0)
            {
                return Result.Error(result.Errors);
            }
            return Result.Invalid(result.ValidationErrors?.ToList());
        }
        return Result.Success(result.Value!);
    }
}
