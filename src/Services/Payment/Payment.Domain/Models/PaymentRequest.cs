using Payment.Domain.Enums;
using Payment.Domain.Exceptions;

namespace Payment.Domain.Models;

public class PaymentRequest
{
    public int Id { get; protected set; }
    public int UserId { get; protected set; }
    public int CourseId { get; protected set; }

    public PaymentState PaymentState { get; protected set; }

    public string? RejectReason { get; protected set; }
    public string? ModifyAdminEmail { get; protected set; }

    protected PaymentRequest() {}
    
    public PaymentRequest(int id, int userId, int courseId)
    {
        Id = id;
        if (userId < 0) throw new ValidationDataException($"Invalid: \"{nameof(userId)}\", can't be less then 0.");
        if (courseId < 0) throw new ValidationDataException($"Invalid: \"{nameof(courseId)}\", can't be less then 0.");
        UserId = userId;
        CourseId = courseId;
        PaymentState = PaymentState.Opened;
        RejectReason = null;
        ModifyAdminEmail = null;
    }

    public void AcceptPayment(string adminEmail)
    {
        ModifyAdminEmail = adminEmail!;
        PaymentState = PaymentState.Confirmed;
    }

    public void RejectPayment(string reason, string adminEmail)
    {
        ModifyAdminEmail = adminEmail!;
        RejectReason = reason!;
        PaymentState = PaymentState.Rejected;
    }
}