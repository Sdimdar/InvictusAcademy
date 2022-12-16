using Ardalis.Result;
using MediatR;
using Payment.Domain.Models;
using ServicesContracts.Payments.Response;

namespace ServicesContracts.Payments.Queries;

public class GetHistoryByPaymentIdQuery : IRequest<Result<List<PaymentHistoryDbModel?>>>
{
    public int PaymentId { get; set; }
}