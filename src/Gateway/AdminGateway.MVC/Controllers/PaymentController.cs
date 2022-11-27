using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Payments.Commands;
using ServicesContracts.Payments.Models;
using ServicesContracts.Payments.Queries;
using ServicesContracts.Payments.Response;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;

[Route("AdminPanel/[controller]/[action]")]
public class PaymentController : Controller
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получить данные о запросе на платёж",
        Description = "Необходимо передать в строке запроса Id платежа"
    )]
    public async Task<ActionResult<DefaultResponseObject<PaymentVm>>> GetById([FromQuery] int paymentId ,
                                                                              CancellationToken cancellationToken)
    {
        GetPaymentQuery query = new()
        {
            PaymentId = paymentId
        };
        var response = await _paymentService.GetByIdPaymentRequestAsync(query, cancellationToken);
        return Ok(response);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение списка запросов по параметрам",
        Description = "Необходимо передать в сроке запроса при необходимости Id пользователя или Id курса," +
                      "а также можно передать тип запроса на оплату"
    )]
    public async Task<ActionResult<DefaultResponseObject<PaymentsPaginationVm>>> GetWithParametersPayment
                            ([FromQuery] GetPaymentsWithParametersQuery request, CancellationToken cancellationToken) {
        var response = await _paymentService.GetWithParametersPaymentRequestAsync(request, cancellationToken);
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
        var response = await _paymentService.AddPaymentRequestAsync(request, cancellationToken);
        return Ok(response);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Подтверждение платежа",
        Description = "Необходимо передать в теле запроса Id платежа"
    )]
    [Authorize]
    public async Task<ActionResult<DefaultResponseObject<bool>>> Confirm([FromBody]PaymentCommon request, 
                                                                         CancellationToken cancellationToken)
    {
        ConfirmPaymentCommand query = new()
        {
            PaymentId = request.PaymentId,
            AdminEmail = User.Identity.Name
        };
        var response = await _paymentService.ConfirmPaymentRequestAsync(query, cancellationToken);
        return Ok(response);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Отклонение платежа",
        Description = "Необходимо передать в теле запроса Id платежа и Email админа отклонившего платёж." +
                      "А также строку с объяснением почему платёж был отклонён."
    )]
    [Authorize]
    public async Task<ActionResult<DefaultResponseObject<bool>>> Reject([FromBody]PaymentCommon request,
                                                                        CancellationToken cancellationToken)
    {
        RejectPaymentCommand query = new()
        {
            PaymentId = request.PaymentId,
            AdminEmail = User.Identity.Name,
            RejectReason = request.RejectReason
        };
        var response = await _paymentService.RejectPaymentRequestAsync(query, cancellationToken);
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
        var response = await _paymentService.GetPaymentsCount(request, cancellationToken);
        return Ok(response);
    }
}