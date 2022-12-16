using CommonRepository.Models;
using Payment.Domain.Enums;

namespace Payment.Domain.Models;

public class PaymentHistoryDbModel : BaseRepositoryEntity
{
    public int PaymentId { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public PaymentState PaymentState { get; set; }
    public string? RejectReason { get; set; }
    public string? ModifyAdminEmail { get; set; }
}