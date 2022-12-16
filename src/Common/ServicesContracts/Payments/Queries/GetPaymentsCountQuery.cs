using Ardalis.Result;
using MediatR;
using Payment.Domain.Enums;

namespace ServicesContracts.Payments.Queries;

public class GetPaymentsCountQuery : IRequest<Result<int>>
{
    public PaymentState PaymentState { get; set; }
}