using Ardalis.Result;
using MediatR;
using Payment.Domain.Models;

namespace ServicesContracts.Payments.Queries;

public class GetPaymentQuery : IRequest<Result<PaymentRequest?>>
{
    public int PaymentId { get; set; }
}