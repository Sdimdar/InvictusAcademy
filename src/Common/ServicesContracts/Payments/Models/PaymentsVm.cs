using Payment.Domain.Enums;

namespace ServicesContracts.Payments.Models;

public class PaymentsVm
{
    public int Id { get; set; }
    public string UserEmail { get; set; }
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public PaymentState PaymentState { get; set; }
    public string? RejectReason { get; set; }
    public string? ModifyAdminEmail { get; set; }
}