using Payment.Domain.Enums;

namespace ServicesContracts.Payments.Response;

public class PaymentHistoryVm
{
    public int PaymentId { get; set; }
    public int UserId { get; set; }
    public string UserEmail { get; set; }
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public PaymentState PaymentState { get; set; }
    public string? RejectReason { get; set; }
    public string? ModifyAdminEmail { get; set; }
    public DateTime CreatedDate { get; set; }
}