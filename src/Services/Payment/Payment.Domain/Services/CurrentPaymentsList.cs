using Payment.Domain.Contracts;
using Payment.Domain.Enums;
using Payment.Domain.Models;

namespace Payment.Domain.Services;

public class CurrentPaymentsList : IDisposable
{
    private readonly List<PaymentRequest> _currentPaymentRequests;
    private int _lastIndex;
    private readonly IPaymentRepository _paymentRepository;

    public CurrentPaymentsList(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
        _currentPaymentRequests = _paymentRepository.GetCurrentRequests();
        _lastIndex = _paymentRepository.GetLastIndex();
    }

    public void AddPaymentRequest(int userId, int courseId)
    {
        if (GetCurrentPaymentRequests(userId, courseId).Count != 0) 
            throw new InvalidOperationException("This payment is already exists");
        ++_lastIndex;
        _currentPaymentRequests.Add(new PaymentRequest(_lastIndex, userId, courseId));
    }

    public void AcceptPayment(int paymentId, string adminEmail)
    {
        var paymentRequest = GetCurrentPaymentRequestById(paymentId);
        paymentRequest!.AcceptPayment(adminEmail);
        _paymentRepository.SavePayment(paymentRequest);
        _currentPaymentRequests.Remove(paymentRequest);
    }

    public void RejectPayment(int paymentId, string rejectReason, string adminEmail)
    {
        var paymentRequest = GetCurrentPaymentRequestById(paymentId);
        paymentRequest!.RejectPayment(rejectReason, adminEmail);
        _paymentRepository.SavePayment(paymentRequest);
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
                                                                    PaymentState paymentState = PaymentState.Opened)
    {
        if (paymentState == PaymentState.Opened) return GetCurrentPaymentRequests(userId, courseId);
        
        var closedPaymentRequests = _paymentRepository.GetPaymentRequestsAsync(userId, courseId);
        var query = _currentPaymentRequests.AsQueryable().Where(e => e.PaymentState == paymentState);
        if (userId is not null) query = query.Where(e => e.UserId == userId);
        if (courseId is not null) query = query.Where(e => e.CourseId == courseId);
        var result = query.ToList();
        result.AddRange(await closedPaymentRequests);
        return result;
    }

    public void Dispose()
    {
        _paymentRepository.SaveAllPayments(_currentPaymentRequests);
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