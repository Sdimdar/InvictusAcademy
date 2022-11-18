using MediatR;
using Payment.Domain.Enums;
using ServicesContracts.Payments.Models;

namespace ServicesContracts.Payments.Queries;

public class GetPaymentsWithParametersQuery : IRequest<List<PaymentVm>>
{
    public int? UserId { get; set; }
    public int? CourseId { get; set; }
    public PaymentState? Status { get; set; }
}