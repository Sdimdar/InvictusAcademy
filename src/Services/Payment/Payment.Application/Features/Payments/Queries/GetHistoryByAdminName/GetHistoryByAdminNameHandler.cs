using Ardalis.Result;
using FluentValidation;
using MediatR;
using Payment.Domain.Contracts;
using Payment.Domain.Models;
using ServicesContracts.Payments.Queries;

namespace Payment.Application.Features.Payments.Queries.GetHistoryByAdminName;

public class GetHistoryByAdminNameHandler:IRequestHandler<GetHistoryByAdminNameQuery, Result<List<PaymentHistoryDbModel?>>>
{
    private readonly IPaymentHistoryRepository _historyRepository;
    private readonly IValidator<GetHistoryByPaymentIdQuery> _validator;

    public GetHistoryByAdminNameHandler(IPaymentHistoryRepository historyRepository, IValidator<GetHistoryByPaymentIdQuery> validator)
    {
        _historyRepository = historyRepository;
        _validator = validator;
    }

    public async Task<Result<List<PaymentHistoryDbModel?>>> Handle(GetHistoryByAdminNameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _historyRepository.GetHistoryByLoginAsync(request.AdminEmail);
            return Result.Success(result);
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