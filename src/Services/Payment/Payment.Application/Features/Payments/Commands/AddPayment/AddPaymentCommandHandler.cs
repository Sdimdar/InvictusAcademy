using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Payment.Domain.Services;
using ServicesContracts.Payments.Commands;

namespace Payment.Application.Features.Payments.Commands.AddPayment;

public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, Result<bool>>
{
    private readonly PaymentService _paymentService;
    private readonly IValidator<AddPaymentCommand> _validator;
    private readonly ILogger<AddPaymentCommandHandler> _logger;

    public AddPaymentCommandHandler(PaymentService paymentService, IValidator<AddPaymentCommand> validator, ILogger<AddPaymentCommandHandler> logger)
    {
        _paymentService = paymentService;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        try
        {
            await _paymentService.AddPaymentRequestAsync(request.UserId, request.CourseId);
            return Result.Success(true);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning($"{BussinesErrors.InvalidCastException.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.InvalidCastException.ToString()}: {ex.Message}");
        }
        catch (NullReferenceException ex)
        {
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Payment with this Id not found");
            return Result.Error($"{BussinesErrors.NotFound.ToString()}: Payment with this Id not found");
        }
    }
}