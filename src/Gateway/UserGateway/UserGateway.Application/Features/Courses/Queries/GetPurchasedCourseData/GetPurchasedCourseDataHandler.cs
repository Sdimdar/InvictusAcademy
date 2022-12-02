using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.Courses.Queries.GetPurchasedCourseData;

public class GetPurchasedCourseDataHandler : IRequestHandler<GetPurchasedCourseDataQuery, Result<PurchasedCourseInfoVm>>
{
    private readonly ICoursesService _coursesService;
    private readonly IValidator<GetPurchasedCourseDataQuery> _validator;

    public GetPurchasedCourseDataHandler(ICoursesService coursesService, IValidator<GetPurchasedCourseDataQuery> validator)
    {
        _coursesService = coursesService;
        _validator = validator;
    }

    public async Task<Result<PurchasedCourseInfoVm>> Handle(GetPurchasedCourseDataQuery request,
                                                            CancellationToken cancellationToken)
    {
        var validationData = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationData.IsValid)
        {
            return Result.Invalid(validationData.AsErrors());
        }

        GetPurchasedCourseDataQuery query = new()
        {
            UserId = request.UserId,
            CourseId = request.CourseId
        };
        var data = await _coursesService.GetPurchasedCourseInfo(query, cancellationToken);
        try
        {

            if (!data.IsSuccess)
            {
                if (data.Errors!.Length != 0) return Result.Error(data.Errors);
                return Result.Invalid(data.ValidationErrors!.ToList());
            }

            return Result.Success(data.Value!);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
