using Payment.Domain.Enums;

namespace ServicesContracts.Payments.Models;

public class PaymentVm
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }

    public PaymentState PaymentState { get; set; }

    public string? RejectReason { get; set; }
    public string? ModifyAdminEmail { get; set; }
}