using CommonRepository.Models;
using Payment.Domain.Enums;

namespace Payment.Infrastructure.Persistence.Models;

public class PaymentRequestDbModel : BaseRepositoryEntity
{
    public string UserEmail { get; set; }
    public int CourseId { get; set; }
    public PaymentState PaymentState { get; set; }
    public string? RejectReason { get; set; }
    public string? ModifyAdminEmail { get; set; }
}