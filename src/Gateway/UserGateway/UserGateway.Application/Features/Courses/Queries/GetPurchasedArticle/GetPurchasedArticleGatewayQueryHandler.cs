using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.Courses.Queries.GetPurchasedArticle;

public class GetPurchasedArticleGatewayQueryHandler : IRequestHandler<GetPurchasedArticleQuery, Result<PurchasedArticleInfoVm>>
{
    private readonly ICoursesService _coursesService;
    private readonly IValidator<GetPurchasedArticleQuery> _validator;

    public GetPurchasedArticleGatewayQueryHandler(ICoursesService coursesService,
                                                  IValidator<GetPurchasedArticleQuery> validator)
    {
        _coursesService = coursesService;
        _validator = validator;
    }

    public async Task<Result<PurchasedArticleInfoVm>> Handle(GetPurchasedArticleQuery request,
                                                             CancellationToken cancellationToken)
    {
        var validationResults = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResults.IsValid)
        {
            return Result.Invalid(validationResults.AsErrors());
        }

        var result = await _coursesService.GetPurchasedArticleInfo(request, cancellationToken);
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
