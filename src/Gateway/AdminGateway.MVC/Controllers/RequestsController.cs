using AdminGateway.MVC.Models;
using AdminGateway.MVC.Services.Interfaces;
using CommonStructures;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Request.Requests.Commands;
using ServicesContracts.Request.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;

[Route("AdminPanel/[controller]/[action]")]
public class RequestsController : Controller
{
    private readonly IRequestService _requestService;
    private readonly ILogger<RequestsController> _logger;
    public RequestsController(IRequestService requestService, ILogger<RequestsController> logger)
    {
        _requestService = requestService;
        _logger = logger;
    }

    [Authorize(Roles = $"{RolesHelper.Administrator},{RolesHelper.Manager}")]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Возвращает список запросов постранично, если передать страницу 0, вернет всех",
        Description = "Необходимо передать номер страницы и количество на странице")
    ]
    public async Task<ActionResult<DefaultResponseObject<GetAllRequestVm>>> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"pageNumber {pageNumber}" + $"pageSize {pageSize}");
        var response = await _requestService.GetAllRequestsAsync(pageNumber, pageSize);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"PageNumber {response.Value.PageNumber}" +
                               $"PageSize {response.Value.PageSize}" +
                               $"TotalPages {response.Value.TotalPages}" +
                               $"Requests Count {response.Value.Requests.Count}");
        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Возвращает количество записей Request , для пагинации")
    ]
    public async Task<ActionResult<DefaultResponseObject<int>>> GetRequestsCount()
    {
        var response = await _requestService.GetRequestsCountAsync();
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}" + 
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"isSucces {response.IsSuccess}" + $"Count {response.Value}");
        return Ok(response);
    }
    
    [Authorize(Roles = $"{RolesHelper.Administrator},{RolesHelper.Manager}")]
    [HttpPost]
    [SwaggerOperation(
        Summary = "Изменяет статус для Request обзвонен/необзвонен",
        Description = "Необходимо передать id")
    ]
    public async Task<ActionResult<DefaultResponseObject<string>>> ChangeCalled([FromBody] ChangeCalledStatusCommand command)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"Id {command.Id}");
        var response = await _requestService.ChangeCalledStatusAsync(command);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"");
        return Ok(response);

    }

    [Authorize(Roles = $"{RolesHelper.Administrator},{RolesHelper.Manager}")]
    [HttpPost]
    [SwaggerOperation(
        Summary = "Добавляет комментарий",
        Description = "Необходимо передать id и комментарий")
    ]
    public async Task<ActionResult<DefaultResponseObject<string>>> ManagerComment([FromBody] ManagerCommentCommand command)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"Id {command.Id}" + $"ManagerComment {command.ManagerComment}");
        var response = await _requestService.ManagerCommentAsync(command);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" + 
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"isSucces {response.IsSuccess}" + $"Value {response.Value}");
        return Ok(response);
    }
}

