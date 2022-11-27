using Payment.Domain.Enums;
using Payment.Domain.Models;

namespace Payment.Domain.Contracts;

public interface IPaymentRepository
{
    Task<int> GetLastIndexAsync();
    List<PaymentRequest> GetCurrentRequestsAsync();
    Task<PaymentRequest> SavePaymentAsync(PaymentRequest paymentRequest);
    Task<List<PaymentRequest>> GetPaymentRequestsAsync(int pageSize, int page, PaymentState paymentState);
    Task<PaymentRequest?> GetPaymentRequestByIdAsync(int id);
    Task<int> GetPaymentsCount(PaymentState paymentState);
}