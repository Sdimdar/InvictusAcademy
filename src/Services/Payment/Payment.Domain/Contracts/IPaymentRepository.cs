using Payment.Domain.Models;

namespace Payment.Domain.Contracts;

public interface IPaymentRepository
{
    List<PaymentRequest> GetCurrentRequests();
    int GetLastIndex();
    void SavePayment(PaymentRequest paymentRequest);
    void SaveAllPayments(List<PaymentRequest> currentPaymentRequests);
    Task<List<PaymentRequest>> GetPaymentRequestsAsync(int? userId, int? courseId);
    Task<PaymentRequest?> GetPaymentRequestByIdAsync(int id);
}