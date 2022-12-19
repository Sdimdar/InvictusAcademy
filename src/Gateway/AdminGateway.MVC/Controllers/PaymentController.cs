using AdminGateway.MVC.Models;
using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using CommonStructures;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Payments.Commands;
using ServicesContracts.Payments.Models;
using ServicesContracts.Payments.Queries;
using ServicesContracts.Payments.Response;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;

[Authorize(Roles = $"{RolesHelper.Administrator},{RolesHelper.Manager}")]
[Route("AdminPanel/[controller]/[action]")]
public class PaymentController : Controller
{
    private readonly IPaymentService _paymentService;
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
    {
        _paymentService = paymentService;
        _logger = logger;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Получить данные о запросе на платёж",
        Description = "Необходимо передать в строке запроса Id платежа"
    )]
    public async Task<ActionResult<DefaultResponseObject<PaymentVm>>> GetById([FromQuery] int paymentId,
                                                                              CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"paymentId {paymentId}");
        GetPaymentQuery query = new()
        {
            PaymentId = paymentId
        };
        var response = await _paymentService.GetByIdPaymentRequestAsync(query, cancellationToken);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Id {response.Value.Id}" +
                               $"CourseId {response.Value.CourseId}" +
                               $"UserId {response.Value.UserId}" +
                               $"PaymentState {response.Value.PaymentState}" +
                               $"RejectReason {response.Value.RejectReason}" +
                               $"ModifyAdminEmail {response.Value.ModifyAdminEmail}" +
                               $"");
        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение списка запросов по параметрам",
        Description = "Необходимо передать в сроке запроса при необходимости Id пользователя или Id курса," +
                      "а также можно передать тип запроса на оплату"
    )]
    public async Task<ActionResult<DefaultResponseObject<PaymentsPaginationVm>>> GetWithParametersPayment
                            ([FromQuery] GetPaymentsWithParametersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"Status {request.Status}" + $"PageNumber {request.PageNumber}" + $"PageSize {request.PageSize}");
        var response = await _paymentService.GetWithParametersPaymentRequestAsync(request, cancellationToken);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Payments Count {response.Value.Payments.Count}" +
                               $"PageNumber {response.Value.PageNumber}" +
                               $"PageSize {response.Value.PageSize}" +
                               $"TotalPages {response.Value.TotalPages}" +
                               $"");
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Создание запроса на платёж",
        Description = "Необходимо передать в теле запроса Id курса и Id пользователя"
    )]
    public async Task<ActionResult<DefaultResponseObject<bool>>> Add([FromBody] AddPaymentCommand request,
                                                                     CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"CourseId {request.CourseId}" + $"UserId {request.UserId}");
        var response = await _paymentService.AddPaymentRequestAsync(request, cancellationToken);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}");
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Подтверждение платежа",
        Description = "Необходимо передать в теле запроса Id платежа"
    )]
    [Authorize]
    public async Task<ActionResult<DefaultResponseObject<bool>>> Confirm([FromBody] PaymentCommon request,
                                                                         CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"PaymentId {request.PaymentId}" + $"RejectReason {request.RejectReason}");
        ConfirmPaymentCommand query = new()
        {
            PaymentId = request.PaymentId,
            AdminEmail = User.Identity.Name
        };
        var response = await _paymentService.ConfirmPaymentRequestAsync(query, cancellationToken);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"");
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Отклонение платежа",
        Description = "Необходимо передать в теле запроса Id платежа и Email админа отклонившего платёж." +
                      "А также строку с объяснением почему платёж был отклонён."
    )]
    [Authorize]
    public async Task<ActionResult<DefaultResponseObject<bool>>> Reject([FromBody] PaymentCommon request,
                                                                        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"PaymentId {request.PaymentId}" + $"RejectReason {request.RejectReason}");
        RejectPaymentCommand query = new()
        {
            PaymentId = request.PaymentId,
            AdminEmail = User.Identity.Name,
            RejectReason = request.RejectReason
        };
        var response = await _paymentService.RejectPaymentRequestAsync(query, cancellationToken);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"");
        return Ok(response);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение списка запросов по параметрам",
        Description = "Необходимо передать в сроке запроса при необходимости Id пользователя или Id курса," +
                      "а также можно передать тип запроса на оплату"
    )]
    public async Task<ActionResult<DefaultResponseObject<int>>> GetPaymentCount
        ([FromQuery] GetPaymentsCountQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"PaymentState {request.PaymentState}");
        var response = await _paymentService.GetPaymentsCount(request, cancellationToken);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"");
        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Запрос на получение истории действий менеджера",
        Description = "Необходимо передать admin email ")
    ]
    public async Task<ActionResult<DefaultResponseObject<List<PaymentHistoryVm>>>> GetHistoryByAdminName(
        [FromQuery] GetHistoryByAdminNameQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"AdminEmail {request.AdminEmail}");
        var response = await _paymentService.GetHistoryByAdminNameAsync(request, cancellationToken);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Count {response.Value.Count}");
        return Ok(response);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Запрос на получение истории действий по заявке на покупку",
        Description = "Необходимо передать id заявки на оплату ")
    ]
    public async Task<ActionResult<DefaultResponseObject<List<PaymentHistoryVm>>>> GetHistoryByPaymentId(
        [FromQuery] GetHistoryByPaymentIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"PaymentId {request.PaymentId}");
        var response = await _paymentService.GetHistoryByPaymentId(request, cancellationToken);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Count {response.Value.Count}"+
                               $"");
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Отклонение одобренного платежа",
        Description = "Необходимо передать в теле запроса Id платежа и Email админа отклонившего платёж." +
                      "А также строку с объяснением почему платёж был отклонён.")
    ]
    public async Task<ActionResult<DefaultResponseObject<bool>>> CancelPayment([FromBody] CancelPaymentCommand request,
        CancellationToken cancellationToken)
    {
        request.AdminEmail = User.Identity.Name;
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"AdminEmail {request.AdminEmail}" + $"PaymentId {request.PaymentId}" + $"PaymentId {request.RejectReason}");
        var response = await _paymentService.CancelPaymentAsync(request, cancellationToken);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"isSucces {response.IsSuccess}" + $"");
        return Ok(response);
    }


}