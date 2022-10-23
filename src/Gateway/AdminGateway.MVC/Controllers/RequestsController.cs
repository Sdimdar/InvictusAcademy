using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Request.Requests.Commands;

namespace AdminGateway.MVC.Controllers;

public class RequestsController : Controller
{
    private readonly IRequestService _requestService;
    public RequestsController(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
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
    public async Task<ActionResult> GetRequestsCount()
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
    public async Task<IActionResult> ChangeCalled(ChangeCalledStatusCommand command)
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
    public async Task<IActionResult> ManagerComment(ManagerCommentCommand command)
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

