using AdminGateway.MVC.Services.Interfaces;
using AutoMapper;
using DataTransferLib.Models;
using ExtendedHttpClient;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using ServicesContracts.Identity.Responses;
using ServicesContracts.Payments.Commands;
using ServicesContracts.Payments.Models;
using ServicesContracts.Payments.Queries;

namespace AdminGateway.MVC.Services;

public class PaymentService : IPaymentService
{
    public ExtendedHttpClient<IPaymentService> ExtendedHttpClient { get; set; }
    public ExtendedHttpClient<ICoursesService> CourseHttpClient { get; set; }
    public ExtendedHttpClient<IGetUsers> UsersHttpClient { get; set; }
    public PaymentService(ExtendedHttpClient<IPaymentService> httpClient, ExtendedHttpClient<ICoursesService> courseHttpClient, ExtendedHttpClient<IGetUsers> usersHttpClient)
    {
        ExtendedHttpClient = httpClient;
        CourseHttpClient = courseHttpClient;
        UsersHttpClient = usersHttpClient;
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
        return await ExtendedHttpClient.PostAndReturnResponseAsync<ConfirmPaymentCommand, DefaultResponseObject<bool>>
            (request, "/Payments/Confirm", cancellationToken);
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
            $"/Payments/GetWithParameters?UserEmail={request.UserId}&CourseId={request.CourseId}&Status={request.Status}",
            cancellationToken);
        if (!payments.IsSuccess) return payments;
        List<int> list = new();
        foreach (var item in payments.Value)
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
        foreach (var item in payments.Value)
        {
            list.Add(item.UserId);
        }

        var test = internalRequest;
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
        //
        // var result = payments.Value.Join(coursesNames.Value, 
        //                                                      payment => payment.CourseId, 
        //                                                      courses => courses.Id, 
        //                                                      (payment, course) =>
        //                                                         {
        //                                                             var paymentResult = payment;
        //                                                             paymentResult.CourseName = course.Name;
        //                                                             return paymentResult;
        //                                                         });
        //
        //
        foreach (var item in payments.Value)
        {
            foreach (var user in usersEmails.Value)
            {
                if (item.UserId == user.Id) item.UserEmail = user.Email;
            }
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