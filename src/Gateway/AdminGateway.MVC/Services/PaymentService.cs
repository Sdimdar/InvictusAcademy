using AdminGateway.MVC.Services.Interfaces;
using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;
using ExtendedHttpClient;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using ServicesContracts.Payments.Commands;
using ServicesContracts.Payments.Models;
using ServicesContracts.Payments.Queries;

namespace AdminGateway.MVC.Services;

public class PaymentService : IPaymentService
{
    public ExtendedHttpClient<IPaymentService> ExtendedHttpClient { get; set; }
    public ExtendedHttpClient<ICoursesService> CourseHttpClient { get; set; }
    private readonly IMapper _mapper;
    
    public PaymentService(ExtendedHttpClient<IPaymentService> httpClient, 
                          ExtendedHttpClient<ICoursesService> courseHttpClient, 
                          IMapper mapper)
    {
        ExtendedHttpClient = httpClient;
        CourseHttpClient = courseHttpClient;
        _mapper = mapper;
    }
    
    public async Task<DefaultResponseObject<bool>> AddPaymentRequestAsync(AddPaymentCommand request, 
                                                                          CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<AddPaymentCommand, DefaultResponseObject<bool>>
            (request, "/Payments/Add", cancellationToken);
    }

    public async Task<DefaultResponseObject<bool>> ConfirmPaymentRequestAsync(ConfirmPaymentCommand request, 
                                                                              CancellationToken cancellationToken)
    {
        var paymentConfirmResult = await ExtendedHttpClient.PostAndReturnResponseAsync
            <ConfirmPaymentCommand, DefaultResponseObject<bool>>(request, "/Payments/Confirm", cancellationToken);
        if (!paymentConfirmResult.IsSuccess) return paymentConfirmResult;
        
        GetPaymentQuery query = new()
        {
            PaymentId = request.PaymentId
        };
        var paymentInfo = await GetByIdPaymentRequestAsync(query, cancellationToken);
        if (!paymentInfo.IsSuccess)
        {
            if (paymentInfo.Errors is not null)
            {
                return _mapper.Map<DefaultResponseObject<bool>>(Result.Error(paymentInfo.Errors));
            }
            return _mapper.Map<DefaultResponseObject<bool>>(Result.Invalid(paymentInfo.ValidationErrors!.ToList()));
        }
        PurchaseCourseCommand purchaseCourseCommand = new()
        {
            CourseId = paymentInfo.Value!.CourseId,
            //TODO: Раскомментировать после того как UserEmail будет заменён на UserId
            //UserId = paymentInfo.Value!.UserId
        };
        return await CourseHttpClient.PostAndReturnResponseAsync
            <PurchaseCourseCommand, DefaultResponseObject<bool>>(purchaseCourseCommand, "/Course/Purchase", cancellationToken);
        
    }

    public async Task<DefaultResponseObject<bool>> RejectPaymentRequestAsync(RejectPaymentCommand request, 
                                                                             CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<RejectPaymentCommand, DefaultResponseObject<bool>>
            (request, "/Payments/Reject", cancellationToken);
    }

    public async Task<DefaultResponseObject<PaymentVm>> GetByIdPaymentRequestAsync(GetPaymentQuery request, 
                                                                                   CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<DefaultResponseObject<PaymentVm>>
            ($"/Payments/Get?PaymentId={request.PaymentId}", cancellationToken);
    }

    public async Task<DefaultResponseObject<List<PaymentsVm>>> GetWithParametersPaymentRequestAsync(GetPaymentsWithParametersQuery request, 
                                                                                                    CancellationToken cancellationToken)
    {
        var payments = await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<List<PaymentsVm>>>(
            $"/Payments/GetWithParameters?UserEmail={request.UserEmail}&CourseId={request.CourseId}&Status={request.Status}",
            cancellationToken);
        if (!payments.IsSuccess) return payments;
        List<int> list = new();
        foreach (var item in payments.Value)
        {
            list.Add(item.CourseId);
        }
        GetCoursesByIdListQuery internalRequest = new GetCoursesByIdListQuery { CoursesId = list };
        var coursesNames = await CourseHttpClient.PostAndReturnResponseAsync<GetCoursesByIdListQuery, DefaultResponseObject<List<CoursesByIdVm>>>(internalRequest,
            "/Course/GetCoursesById", cancellationToken);
        if (!coursesNames.IsSuccess)
        {
            if (coursesNames.Errors.Any()) payments.Errors = coursesNames.Errors;
            if (coursesNames.ValidationErrors.Any()) payments.ValidationErrors = coursesNames.ValidationErrors;
            return payments;
        }
        foreach (var item in payments.Value)
        {
            foreach (var course in coursesNames.Value)
            {
                if (item.CourseId == course.Id) item.CourseName = course.Name;
            }
        }

        return payments;
    }
}