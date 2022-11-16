using Payment.Domain.Enums;
using Payment.Domain.Models;

namespace Payment.Domain.Contracts;

public interface IPaymentRepository
{
    List<PaymentRequest> GetCurrentRequests();
    int GetLastIndex();
    Task SavePaymentAsync(PaymentRequest paymentRequest);
    Task SaveAllPaymentsAsync(List<PaymentRequest> currentPaymentRequests);
    Task<List<PaymentRequest>> GetPaymentRequestsAsync(int? userId, int? courseId, PaymentState? paymentState);
    Task<PaymentRequest?> GetPaymentRequestByIdAsync(int id);
}