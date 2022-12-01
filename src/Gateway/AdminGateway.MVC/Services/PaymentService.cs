using AdminGateway.MVC.Services.Interfaces;
using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;
using ExtendedHttpClient;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using ServicesContracts.Identity.Responses;
using ServicesContracts.Payments.Commands;
using ServicesContracts.Payments.Models;
using ServicesContracts.Payments.Queries;
using ServicesContracts.Payments.Response;

namespace AdminGateway.MVC.Services;

public class PaymentService : IPaymentService
{
    public ExtendedHttpClient<IPaymentService> ExtendedHttpClient { get; set; }
    public ExtendedHttpClient<ICoursesService> CourseHttpClient { get; set; }
    public ExtendedHttpClient<IGetUsers> UsersHttpClient { get; set; }
    private readonly IMapper _mapper;
    
    public PaymentService(ExtendedHttpClient<IPaymentService> httpClient, 
                          ExtendedHttpClient<ICoursesService> courseHttpClient,
                          ExtendedHttpClient<IGetUsers> usersHttpClient,
                          IMapper mapper)
    {
        ExtendedHttpClient = httpClient;
        CourseHttpClient = courseHttpClient;
        UsersHttpClient = usersHttpClient;
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
            UserId = paymentInfo.Value!.UserId
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
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<PaymentVm>>
            ($"/Payments/Get?PaymentId={request.PaymentId}", cancellationToken);
    }

    public async Task<DefaultResponseObject<int>> GetPaymentsCount(GetPaymentsCountQuery request,
        CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<int>>
            ($"/Payments/Count?PaymentState={request.PaymentState}");
    }

    public async Task<DefaultResponseObject<PaymentsPaginationVm>> GetWithParametersPaymentRequestAsync(GetPaymentsWithParametersQuery request, 
                                                                                                    CancellationToken cancellationToken)
    {
        var payments = await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<PaymentsPaginationVm>>(
            $"/Payments/GetWithParameters?PageNumber={request.PageNumber}&PageSize={request.PageSize}&Status={request.Status}",
            cancellationToken);
        if (!payments.IsSuccess) return payments;
        List<int> list = new();
        foreach (var item in payments.Value.Payments)
        {
            list.Add(item.CourseId);
        }
        GetCoursesNamesByListIdQuery internalRequest = new GetCoursesNamesByListIdQuery { ListId = list };
        var coursesNames = await CourseHttpClient.PostAndReturnResponseAsync<GetCoursesNamesByListIdQuery, DefaultResponseObject<List<CoursesByIdVm>>>(internalRequest,
            "/Course/GetCoursesById");
        if (!coursesNames.IsSuccess)
        {
            if (coursesNames.Errors.Any()) payments.Errors = coursesNames.Errors;
            if (coursesNames.ValidationErrors.Any()) payments.ValidationErrors = coursesNames.ValidationErrors;
            return payments;
        }
        list = new List<int>();
        foreach (var item in payments.Value.Payments)
        {
            list.Add(item.UserId);
        }

        internalRequest.ListId = list;
        var usersEmails =
            await UsersHttpClient.PostAndReturnResponseAsync<GetCoursesNamesByListIdQuery,DefaultResponseObject<List<UsersEmailsByListIdVm>>>(internalRequest,
                "/User/GetUsersById");
        if (!usersEmails.IsSuccess)
        {
            if (usersEmails.Errors.Any()) payments.Errors = usersEmails.Errors;
            if (usersEmails.ValidationErrors.Any()) payments.ValidationErrors = usersEmails.ValidationErrors;
            return payments;
        }
        
        foreach (var item in payments.Value.Payments)
        {
            foreach (var user in usersEmails.Value)
            {
                if (item.UserId == user.Id) item.UserEmail = user.Email;
            }
        }
        foreach (var item in payments.Value.Payments)
        {
            foreach (var course in coursesNames.Value)
            {
                if (item.CourseId == course.Id) item.CourseName = course.Name;
            }
        }

        return payments;
    }

    public async Task<DefaultResponseObject<List<PaymentHistoryVm>>> GetHistoryByAdminNameAsync(
        GetHistoryByAdminNameQuery request, CancellationToken cancellationToken)
    {
        var paymentsHistory = await ExtendedHttpClient
            .GetAndReturnResponseAsync<DefaultResponseObject<List<PaymentHistoryVm>>>(
                $"Payments/GetHistoryByAdminName?AdminEmail={request.AdminEmail}");
        if (!paymentsHistory.IsSuccess) return paymentsHistory;
        paymentsHistory = await GetEmailsAndCourseNames(paymentsHistory);
        return paymentsHistory;
    }

    public async Task<DefaultResponseObject<List<PaymentHistoryVm>>> GetHistoryByPaymentId(
        GetHistoryByPaymentIdQuery request, CancellationToken cancellationToken)
    {
        var paymentsHistory = await ExtendedHttpClient
            .GetAndReturnResponseAsync<DefaultResponseObject<List<PaymentHistoryVm>>>(
                $"Payments/GetHistoryById?PaymentId={request.PaymentId}");
        if (!paymentsHistory.IsSuccess) return paymentsHistory;
        paymentsHistory = await GetEmailsAndCourseNames(paymentsHistory);
        return paymentsHistory;
    }
    
    //Используется только для типа PaymentHistoryVm
    private async Task<DefaultResponseObject<List<PaymentHistoryVm>>> GetEmailsAndCourseNames(
        DefaultResponseObject<List<PaymentHistoryVm>> paymentsHistory)
    {
        List<int> list = new();
        foreach (var item in paymentsHistory.Value)
        {
            list.Add(item.CourseId);
        }
        GetCoursesNamesByListIdQuery internalRequest = new GetCoursesNamesByListIdQuery { ListId = list };
        var coursesNames = await CourseHttpClient.PostAndReturnResponseAsync<GetCoursesNamesByListIdQuery, DefaultResponseObject<List<CoursesByIdVm>>>(internalRequest,
            "/Course/GetCoursesById");
        if (!coursesNames.IsSuccess)
        {
            if (coursesNames.Errors.Any()) paymentsHistory.Errors = coursesNames.Errors;
            if (coursesNames.ValidationErrors.Any()) paymentsHistory.ValidationErrors = coursesNames.ValidationErrors;
            return paymentsHistory;
        }
        list = new List<int>();
        foreach (var item in paymentsHistory.Value)
        {
            list.Add(item.UserId);
        }
        internalRequest.ListId = list;
        var usersEmails =
            await UsersHttpClient.PostAndReturnResponseAsync<GetCoursesNamesByListIdQuery,DefaultResponseObject<List<UsersEmailsByListIdVm>>>(internalRequest,
                "/User/GetUsersById");
        if (!usersEmails.IsSuccess)
        {
            if (usersEmails.Errors.Any()) paymentsHistory.Errors = usersEmails.Errors;
            if (usersEmails.ValidationErrors.Any()) paymentsHistory.ValidationErrors = usersEmails.ValidationErrors;
            return paymentsHistory;
        }
        
        foreach (var item in paymentsHistory.Value)
        {
            foreach (var user in usersEmails.Value)
            {
                if (item.UserId == user.Id) item.UserEmail = user.Email;
            }
        }
        foreach (var item in paymentsHistory.Value)
        {
            foreach (var course in coursesNames.Value)
            {
                if (item.CourseId == course.Id) item.CourseName = course.Name;
            }
        }
        return paymentsHistory;
    }
    
}