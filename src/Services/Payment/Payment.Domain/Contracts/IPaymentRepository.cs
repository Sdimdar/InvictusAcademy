using Payment.Domain.Enums;
using Payment.Domain.Models;

namespace Payment.Domain.Contracts;

public interface IPaymentRepository
{
    Task<int> GetLastIndexAsync();
    List<PaymentRequest> GetCurrentRequestsAsync();
    Task<PaymentRequest> SavePaymentAsync(PaymentRequest paymentRequest);
    Task<List<PaymentRequest>> GetPaymentRequestsAsync(int? userId, int? courseId, PaymentState? paymentState);
    Task<PaymentRequest?> GetPaymentRequestByIdAsync(int id);
}