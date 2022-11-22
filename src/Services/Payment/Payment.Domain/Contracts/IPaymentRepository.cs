using Payment.Domain.Enums;
using Payment.Domain.Models;

namespace Payment.Domain.Contracts;

public interface IPaymentRepository
{
    List<PaymentRequest> GetCurrentRequests();
    int GetLastIndex();
    Task SavePaymentAsync(PaymentRequest paymentRequest);
    Task<List<PaymentRequest>> GetPaymentRequestsAsync(string? userEmail, int? courseId, PaymentState? paymentState);
    Task<PaymentRequest?> GetPaymentRequestByIdAsync(int id);
}