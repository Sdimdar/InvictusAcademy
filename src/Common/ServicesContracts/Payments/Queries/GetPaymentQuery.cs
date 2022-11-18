using MediatR;
using ServicesContracts.Payments.Models;

namespace ServicesContracts.Payments.Queries;

public class GetPaymentQuery : IRequest<PaymentVm>
{
    public int PaymentId { get; set; }
}