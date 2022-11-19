using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Payment.Domain.Services;
using ServicesContracts.Payments.Commands;

namespace Payment.Application.Features.Payments.Commands.RejectPayment;

public class RejectPaymentCommandHandler : IRequestHandler<RejectPaymentCommand, Result<bool>>
{
    private readonly PaymentService _paymentService;
    private readonly IValidator<RejectPaymentCommand> _validator;

    public RejectPaymentCommandHandler(PaymentService paymentService, IValidator<RejectPaymentCommand> validator)
    {
        _paymentService = paymentService;
        _validator = validator;
    }

    public async Task<Result<bool>> Handle(RejectPaymentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        try
        {
            await _paymentService.RejectPaymentAsync(request.PaymentId, request.RejectReason, request.AdminEmail);
            return Result.Success(true);
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