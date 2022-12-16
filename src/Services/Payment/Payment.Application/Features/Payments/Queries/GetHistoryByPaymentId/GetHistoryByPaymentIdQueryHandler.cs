using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Payment.Domain.Contracts;
using Payment.Domain.Models;
using Payment.Domain.Services;
using ServicesContracts.Payments.Queries;
using ServicesContracts.Payments.Response;

namespace Payment.Application.Features.Payments.Queries.GetHistoryByPaymentId;

public class GetHistoryByPaymentIdQueryHandler: IRequestHandler<GetHistoryByPaymentIdQuery, Result<List<PaymentHistoryDbModel?>>>
{
    private readonly IPaymentHistoryRepository _historyRepository;
    private readonly IValidator<GetHistoryByPaymentIdQuery> _validator;


    public GetHistoryByPaymentIdQueryHandler(IPaymentHistoryRepository historyRepository, IValidator<GetHistoryByPaymentIdQuery> validator)
    {
        _historyRepository = historyRepository;
        _validator = validator;
    }

    public async Task<Result<List<PaymentHistoryDbModel?>>> Handle(GetHistoryByPaymentIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        try
        {
            var result = await _historyRepository.GetHistoryByIdAsync(request.PaymentId);
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