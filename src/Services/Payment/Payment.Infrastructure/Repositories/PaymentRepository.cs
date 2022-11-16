using AutoMapper;
using CommonRepository;
using Microsoft.EntityFrameworkCore;
using Payment.Domain.Contracts;
using Payment.Domain.Enums;
using Payment.Domain.Models;
using Payment.Infrastructure.Persistence;
using Payment.Infrastructure.Persistence.Models;

namespace Payment.Infrastructure.Repositories;

public class PaymentRepository : BaseRepository<PaymentRequestDbModel, PaymentDbContext>, IPaymentRepository
{
    private readonly IMapper _mapper;
    
    public PaymentRepository(PaymentDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    protected override IQueryable<PaymentRequestDbModel> FilterByString(IQueryable<PaymentRequestDbModel> query, string? filterString)
    {
        if (filterString is null) return query;
        query = query.Where(e => e.ModifyAdminEmail.Contains(filterString));
        return query;
    }

    public List<PaymentRequest> GetCurrentRequests()
    {
        var dbData = Context.PaymentRequests.Where(e => e.PaymentState == PaymentState.Opened).ToList();
        return _mapper.Map<List<PaymentRequest>>(dbData);
    }

    public int GetLastIndex()
    {
        return Context.PaymentRequests.Max(e => e.Id);
    }

    public async Task SavePaymentAsync(PaymentRequest paymentRequest)
    {
        var data = _mapper.Map<PaymentRequestDbModel>(paymentRequest);
        await UpdateAsync(data);
        await Context.SaveChangesAsync();
    }

    public async Task SaveAllPaymentsAsync(List<PaymentRequest> currentPaymentRequests)
    {
        var data = _mapper.Map<List<PaymentRequestDbModel>>(currentPaymentRequests);
        foreach (var item in data)
        {
            await UpdateAsync(item);
        }
        await Context.SaveChangesAsync();
    }

    public async Task<List<PaymentRequest>> GetPaymentRequestsAsync(int? userId, 
                                                                    int? courseId, 
                                                                    PaymentState? paymentState)
    {
        var query = Context.PaymentRequests.AsQueryable();
        if (userId is not null) query = query.Where(e => e.UserId == userId);
        if (courseId is not null) query = query.Where(e => e.CourseId == courseId);
        if (paymentState is not null) query = query.Where(e => e.PaymentState == paymentState);
        var data = await query.ToListAsync();
        return _mapper.Map<List<PaymentRequest>>(data);
    }

    public async Task<PaymentRequest?> GetPaymentRequestByIdAsync(int id)
    {
        var data = await GetByIdAsync(id);
        return _mapper.Map<PaymentRequest>(data);
    }
}