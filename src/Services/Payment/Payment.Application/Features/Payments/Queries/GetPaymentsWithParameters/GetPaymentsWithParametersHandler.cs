using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Payment.Domain.Models;
using Payment.Domain.Services;
using ServicesContracts.Payments.Queries;

namespace Payment.Application.Features.Payments.Queries.GetPaymentsWithParameters;

public class GetPaymentsWithParametersHandler : IRequestHandler<GetPaymentsWithParametersQuery, Result<List<PaymentRequest>>>
{
    private readonly PaymentService _paymentService;
    private readonly IValidator<GetPaymentsWithParametersQuery> _validator;
    private readonly ILogger<GetPaymentsWithParametersHandler> _logger;

    public GetPaymentsWithParametersHandler(PaymentService paymentService, IValidator<GetPaymentsWithParametersQuery> validator)
    {
        _paymentService = paymentService;
        _validator = validator;
    }

    public async Task<Result<List<PaymentRequest>>> Handle(GetPaymentsWithParametersQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        try
        {
            var result = await _paymentService.GetPaymentRequestsAsync(request.UserId, request.CourseId, request.Status);
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