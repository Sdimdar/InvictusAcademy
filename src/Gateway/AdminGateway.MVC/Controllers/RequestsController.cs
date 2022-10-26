using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Request.Requests.Commands;
using ServicesContracts.Request.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;
[Route("AdminPanel/[controller]/[action]")]
public class RequestsController : Controller
{
    private readonly IRequestService _requestService;
    public RequestsController(IRequestService requestService)
    {
        _requestService = requestService;
    }

    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Возвращает список запросов постранично, если передать страницу 0, вернет всех",
        Description = "Необходимо передать номер страницы и количество на странице")
    ]
    public async Task<ActionResult<DefaultResponseObject<GetAllRequestVm>>> GetAll(int pageNumber = 1, int pageSize = 3)
    {
        try
        {
            var response = await _requestService.GetAllRequestsAsync(pageNumber, pageSize);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return View("../Errors/ErrorPage", error);
        }
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Возвращает количество записей Request , для пагинации")
    ]
    public async Task<ActionResult<DefaultResponseObject<int>>> GetRequestsCount()
    {
        try
        {
            var response = await _requestService.GetRequestsCountAsync();
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return View("../Errors/ErrorPage", error);
        }
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Изменяет статус для Request обзвонен/необзвонен",
        Description = "Необходимо передать id")
    ]
    public async Task<ActionResult<DefaultResponseObject<string>>> ChangeCalled(ChangeCalledStatusCommand command)
    {
        try
        {
            if (command.Id <= 0)
            {
                ErrorVM error = new ErrorVM("Id was not assigned");
                return View("../Errors/ErrorPage", error);
            }
            var response = await _requestService.ChangeCalledStatusAsync(command);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return View("../Errors/ErrorPage", error);
        }
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Добавляет комментарий",
        Description = "Необходимо передать id и комментарий")
    ]
    public async Task<ActionResult<DefaultResponseObject<string>>> ManagerComment(ManagerCommentCommand command)
    {
        try
        {
           
            if (command.Id <= 0)
            {
                ErrorVM error = new ErrorVM("Id was not assigned");
                return View("../Errors/ErrorPage", error);
            }
            var response = await _requestService.ManagerCommentAsync(command);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return View("../Errors/ErrorPage", error);
        }
    }
}

