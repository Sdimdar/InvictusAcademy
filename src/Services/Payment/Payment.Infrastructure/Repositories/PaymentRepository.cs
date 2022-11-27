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

    public async Task<int> GetLastIndexAsync()
    {
        return await Context.PaymentRequests.MaxAsync(p => p.Id);
    }

    public List<PaymentRequest> GetCurrentRequestsAsync()
    {
        var dbData = Context.PaymentRequests.Where(e => e.PaymentState == PaymentState.Opened).ToList();
        return _mapper.Map<List<PaymentRequest>>(dbData);
    }

    public async Task<PaymentRequest> SavePaymentAsync(PaymentRequest paymentRequest)
    {
        var payment = await GetByIdAsync(paymentRequest.Id);
        if (payment is null)
        {
            payment = _mapper.Map<PaymentRequestDbModel>(paymentRequest);
            return _mapper.Map<PaymentRequest>(await AddAsync(payment));
        }
        else
        {
            payment.PaymentState = paymentRequest.PaymentState;
            payment.RejectReason = paymentRequest.RejectReason;
            payment.ModifyAdminEmail = paymentRequest.ModifyAdminEmail;
            await UpdateAsync(payment);
            return _mapper.Map<PaymentRequest>(payment);
        }
    }

    public async Task<List<PaymentRequest>> GetPaymentRequestsAsync(int pageSize, int page, PaymentState paymentState)
    {
        var response = await Context.PaymentRequests.Where(c=>c.PaymentState == paymentState)
            .OrderByDescending(e=>e.LastModifiedDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return _mapper.Map<List<PaymentRequest>>(response);
    }

    public async Task<PaymentRequest?> GetPaymentRequestByIdAsync(int id)
    {
        var data = await GetByIdAsync(id);
        return _mapper.Map<PaymentRequest>(data);
    }

    public async Task<int> GetPaymentsCount(PaymentState paymentState)
    {
        var query = await Context.PaymentRequests.Where(e => e.PaymentState == paymentState).ToListAsync();
        return query.Count();
    }
}