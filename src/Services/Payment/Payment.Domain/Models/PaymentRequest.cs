﻿using Payment.Domain.Enums;
using Payment.Domain.Exceptions;

namespace Payment.Domain.Models;

public class PaymentRequest
{
    public int Id { get; }
    public int UserId { get; }
    public int CourseId { get; }

    public PaymentState PaymentState { get; private set; }

    public string? RejectReason { get; private set; }
    public string? ModifyAdminEmail { get; private set; }

    protected PaymentRequest() {}
    
    public PaymentRequest(int id, int userId, int courseId)
    {
        if (id < 0) throw new ValidationDataException($"Invalid: \"{nameof(id)}\", can't be less then 0.");
        if (userId < 0) throw new ValidationDataException($"Invalid: \"{nameof(userId)}\", can't be less then 0.");
        if (courseId < 0) throw new ValidationDataException($"Invalid: \"{nameof(courseId)}\", can't be less then 0.");
        Id = id;
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