using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Payment.Domain.Models;
using Payment.Domain.Services;
using ServicesContracts.Payments.Queries;

namespace Payment.Application.Features.Payments.Queries.GetPaymentById;

public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentQuery, Result<PaymentRequest?>>
{
    private readonly PaymentService _paymentService;
    private readonly IValidator<GetPaymentQuery> _validator;
    private readonly ILogger<GetPaymentByIdQueryHandler> _logger;

    public GetPaymentByIdQueryHandler(PaymentService paymentService, IValidator<GetPaymentQuery> validator, ILogger<GetPaymentByIdQueryHandler> logger)
    {
        _paymentService = paymentService;
        _validator = validator;
        _logger = logger;
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
            _logger.LogWarning($"{BussinesErrors.InvalidCastException.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.InvalidCastException.ToString()}: {ex.Message}");
        }
        catch (NullReferenceException ex)
        {
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Payment with this Id not found");
            return Result.Error($"{BussinesErrors.NotFound.ToString()}: Payment with this Id not found");;
        }
    }
}