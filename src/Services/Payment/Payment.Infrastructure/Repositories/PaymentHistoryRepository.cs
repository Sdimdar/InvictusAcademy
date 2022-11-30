using CommonRepository;
using Microsoft.EntityFrameworkCore;
using Payment.Domain.Contracts;
using Payment.Domain.Models;
using Payment.Infrastructure.Persistence;

namespace Payment.Infrastructure.Repositories;

public class PaymentHistoryRepository:BaseRepository<PaymentHistoryDbModel, PaymentDbContext>,IPaymentHistoryRepository

{
    
    public PaymentHistoryRepository(PaymentDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<PaymentHistoryDbModel> FilterByString(IQueryable<PaymentHistoryDbModel> query, string? filterString)
    {
        throw new NotImplementedException();
    }
    public async Task<List<PaymentHistoryDbModel>> GetHistoryByIdAsync(int paymentId)
    {
        var query = Context.PaymentHistory.Where(c => c.PaymentId == paymentId);
        return await query.ToListAsync();
    }

    public async Task<List<PaymentHistoryDbModel>> GetHistoryByLoginAsync(string adminEmail)
    {
        var query = Context.PaymentHistory.Where(c => c.ModifyAdminEmail.ToLower() == adminEmail.ToLower());
        return await query.ToListAsync();
    }
    
}