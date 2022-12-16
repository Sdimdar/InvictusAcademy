using CommonRepository.Abstractions;
using Payment.Domain.Models;

namespace Payment.Domain.Contracts;

public interface IPaymentHistoryRepository : IBaseRepository<PaymentHistoryDbModel>
{
    Task<List<PaymentHistoryDbModel>> GetHistoryByIdAsync(int paymentId);
    Task<List<PaymentHistoryDbModel>> GetHistoryByLoginAsync(string adminEmail);
}