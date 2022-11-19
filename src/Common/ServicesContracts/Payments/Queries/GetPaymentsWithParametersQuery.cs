using Ardalis.Result;
using MediatR;
using Payment.Domain.Enums;
using Payment.Domain.Models;

namespace ServicesContracts.Payments.Queries;

public class GetPaymentsWithParametersQuery : IRequest<Result<List<PaymentRequest>>>
{
    public int? UserId { get; set; }
    public int? CourseId { get; set; }
    public PaymentState? Status { get; set; }
}