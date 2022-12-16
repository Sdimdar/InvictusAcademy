using Ardalis.Result;
using MediatR;
using Payment.Domain.Contracts;
using ServicesContracts.Payments.Queries;

namespace Payment.Application.Features.Payments.Queries.GetPaymentsCount;

public class GetPaymentsCountHandler:IRequestHandler<GetPaymentsCountQuery, Result<int>>
{
    private readonly IPaymentRepository _paymentRepository;

    public GetPaymentsCountHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<Result<int>> Handle(GetPaymentsCountQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return Result.Success(await _paymentRepository.GetPaymentsCount(request.PaymentState));
        }
        catch (InvalidOperationException ex)
        {
            return Result.Error(ex.Message);
        }
        catch (NullReferenceException ex)
        {
            return Result.Error("Payment with this Id not found");
        }
    }
}