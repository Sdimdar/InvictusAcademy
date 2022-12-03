using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Requests.Tests.Queries;
using ServicesContracts.Courses.Responses;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.Courses.Queries.GetPurchasedTest;

public class GetPurchasedTestQueryGatewayHandler : IRequestHandler<GetPurchasedTestQuery, Result<List<PurchasedTestVm>>>
{
    private readonly ICoursesService _coursesService;

    public GetPurchasedTestQueryGatewayHandler(ICoursesService coursesService)
    {
        _coursesService = coursesService;
    }

    public async Task<Result<List<PurchasedTestVm>>> Handle(GetPurchasedTestQuery request, CancellationToken cancellationToken)
    {
        var data = await _coursesService.GetPurchasedTestInfo(request, cancellationToken);
        try
        {

            if (!data.IsSuccess)
            {
                if (data.Errors!.Length != 0)
                {
                    return Result.Error(data.Errors);
                }

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
