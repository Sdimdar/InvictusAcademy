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

    public async Task AddPaymentRequestAsync(int userId, int courseId)
    {
        if (GetCurrentPaymentRequests(userId, courseId).Count != 0) 
            throw new InvalidOperationException("This payment is already exists");
        var paymentRequest = new PaymentRequest((await _paymentRepository.GetLastIndexAsync()) + 1, userId, courseId);
        paymentRequest = await _paymentRepository.SavePaymentAsync(paymentRequest);
        _currentPaymentRequests.Add(paymentRequest);
    }

    public async Task AcceptPaymentAsync(int paymentId, string adminEmail)
    {
        var paymentRequest = GetCurrentPaymentRequestById(paymentId);
        paymentRequest!.AcceptPayment(adminEmail);
        await _paymentRepository.SavePaymentAsync(paymentRequest);
        _currentPaymentRequests.Remove(paymentRequest);
    }

    public async Task RejectPaymentAsync(int paymentId, string rejectReason, string adminEmail)
    {
        var paymentRequest = GetCurrentPaymentRequestById(paymentId);
        paymentRequest!.RejectPayment(rejectReason, adminEmail);
        await _paymentRepository.SavePaymentAsync(paymentRequest);
        _currentPaymentRequests.Remove(paymentRequest);
    }

    public async Task<PaymentRequest?> GetPaymentRequestByIdAsync(int id)
    {
        var result = _currentPaymentRequests.FirstOrDefault(e => e.Id == id);
        if (result is not null) return result;

        result = await _paymentRepository.GetPaymentRequestByIdAsync(id);
        return result;
    }

    public async Task<List<PaymentRequest>> GetPaymentRequestsAsync(int? userId = null, 
                                                                    int? courseId = null, 
                                                                    PaymentState? paymentState = null)
    {
        var currentPaymentRequests = GetCurrentPaymentRequests(userId, courseId);
        if (paymentState == PaymentState.Opened) return currentPaymentRequests;
        
        var dbPaymentRequests = _paymentRepository.GetPaymentRequestsAsync(userId, courseId, paymentState);
        return await dbPaymentRequests;
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
}