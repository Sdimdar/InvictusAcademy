using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Payment.Domain.Models;
using Payment.Domain.Services;
using ServicesContracts.Payments.Queries;

namespace Payment.Application.Features.Payments.Queries.GetPaymentById;

public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentQuery, Result<PaymentRequest?>>
{
    private readonly PaymentService _paymentService;
    private readonly IValidator<GetPaymentQuery> _validator;

    public GetPaymentByIdQueryHandler(PaymentService paymentService, IValidator<GetPaymentQuery> validator)
    {
        _paymentService = paymentService;
        _validator = validator;
    }

    public async Task<Result<PaymentRequest?>> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        try
        {
            var result = await _paymentService.GetPaymentRequestByIdAsync(request.PaymentId);
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