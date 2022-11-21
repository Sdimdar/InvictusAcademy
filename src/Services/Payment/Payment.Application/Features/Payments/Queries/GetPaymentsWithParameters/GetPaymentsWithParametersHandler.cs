using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Payment.Domain.Models;
using Payment.Domain.Services;
using ServicesContracts.Payments.Queries;

namespace Payment.Application.Features.Payments.Queries.GetPaymentsWithParameters;

public class GetPaymentsWithParametersHandler : IRequestHandler<GetPaymentsWithParametersQuery, Result<List<PaymentRequest>>>
{
    private readonly PaymentService _paymentService;
    private readonly IValidator<GetPaymentsWithParametersQuery> _validator;

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
            var result = await _paymentService.GetPaymentRequestsAsync(request.UserEmail, request.CourseId, request.Status);
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