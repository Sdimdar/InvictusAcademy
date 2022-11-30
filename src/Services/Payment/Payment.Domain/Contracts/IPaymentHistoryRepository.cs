using Payment.Domain.Models;

namespace Payment.Domain.Contracts;

public interface IPaymentHistoryRepository
{
    Task<List<PaymentHistoryDbModel>> GetHistoryByIdAsync(int paymentId);
    Task<List<PaymentHistoryDbModel>> GetHistoryByLoginAsync(string adminEmail);
}