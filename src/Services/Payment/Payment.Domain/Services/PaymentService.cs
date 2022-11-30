using Microsoft.Extensions.Logging;
using Payment.Domain.Contracts;
using Payment.Domain.Enums;
using Payment.Domain.Models;

namespace Payment.Domain.Services;

public class PaymentService
{
    private readonly List<PaymentRequest> _currentPaymentRequests;
    private readonly IPaymentRepository _paymentRepository;
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(IPaymentRepository paymentRepository, ILogger<PaymentService> logger)
    {
        _paymentRepository = paymentRepository;
        _logger = logger;
        _currentPaymentRequests = _paymentRepository.GetCurrentRequestsAsync();
    }

    public async Task<PaymentRequest> AddPaymentRequestAsync(int userId, int courseId)
    {
        if (GetCurrentPaymentRequests(userId, courseId).Count != 0)
            throw new InvalidOperationException("This payment is already exists");
        int nextId = 0;
        try
        {
            nextId = (await _paymentRepository.GetLastIndexAsync()) + 1;
        }
        catch (Exception ex)
        {
        }

        var paymentRequest = new PaymentRequest(nextId, userId, courseId);
        paymentRequest = await _paymentRepository.SavePaymentAsync(paymentRequest);
        _currentPaymentRequests.Add(paymentRequest);
        return paymentRequest;
    }

    public async Task<PaymentRequest> AcceptPaymentAsync(int paymentId, string adminEmail)
    {
        var paymentRequest = GetCurrentPaymentRequestById(paymentId);
        paymentRequest!.AcceptPayment(adminEmail);
        await _paymentRepository.SavePaymentAsync(paymentRequest);
        _currentPaymentRequests.Remove(paymentRequest);
        return paymentRequest;
    }

    public async Task<PaymentRequest> RejectPaymentAsync(int paymentId, string rejectReason, string adminEmail)
    {
        var paymentRequest = GetCurrentPaymentRequestById(paymentId);
        paymentRequest!.RejectPayment(rejectReason, adminEmail);
        await _paymentRepository.SavePaymentAsync(paymentRequest);
        _currentPaymentRequests.Remove(paymentRequest);
        return paymentRequest;
    }

    public async Task<PaymentRequest?> GetPaymentRequestByIdAsync(int id)
    {
        var result = _currentPaymentRequests.FirstOrDefault(e => e.Id == id);
        if (result is not null) return result;

        result = await _paymentRepository.GetPaymentRequestByIdAsync(id);
        return result;
    }

    public async Task<List<PaymentRequest>> GetPaymentsAsync(int pageSize, int page, PaymentState paymentState)
    {

        var dbPaymentRequests = _paymentRepository.GetPaymentRequestsAsync(pageSize, page, paymentState);
        return await dbPaymentRequests;
    }

    public async Task<int> GetPaymentsCount(PaymentState paymentState)
    {
        return await _paymentRepository.GetPaymentsCount(paymentState);
    }

    private PaymentRequest? GetCurrentPaymentRequestById(int id)
    {
        return _currentPaymentRequests.FirstOrDefault(e => e.Id == id);
    }

    private List<PaymentRequest> GetCurrentPaymentRequests(int? userId = null, int? courseId = null)
    {
        var query = _currentPaymentRequests.AsQueryable();
        if (userId is not null) query = query.Where(e => e.UserId == userId);
        if (courseId is not null) query = query.Where(e => e.CourseId == courseId);
        return query.ToList();
    }
    public async Task<PaymentRequest> CancelPaymentAsync(int paymentId, string rejectReason, string adminEmail)
    {
        var checkPayment = await _paymentRepository.CheckPaymentConfirm(paymentId);
        if (!checkPayment) throw new InvalidOperationException($"Application No. {paymentId} is not paid");
        var paymentRequest = await _paymentRepository.GetPaymentRequestByIdAsync(paymentId);
        paymentRequest!.CancelPayment(rejectReason, adminEmail);
        await _paymentRepository.SavePaymentAsync(paymentRequest);
        return paymentRequest;
    }
    
    
}