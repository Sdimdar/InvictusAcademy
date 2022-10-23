using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Request.Requests.Commands;
using ServicesContracts.Request.Responses;

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
            var responce = await _requestService.GetAllRequestsAsync(pageNumber, pageSize);
            if (responce is null)
            {
                ErrorVM error = new ErrorVM("Requests couldn't find");
                return View("../Errors/ErrorPage", error);
            }
            var responceCount = await _requestService.GetRequestsCountAsync();
            if (responceCount is null)
            {
                ErrorVM error = new ErrorVM("Requests count couldn't find");
                return View("../Errors/ErrorPage", error);
            }
            GetAllRequestVm requests = new GetAllRequestVm
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Requests = responce.Value.Requests,
                TotalPages = (int)Math.Ceiling(responceCount.Value / (double)pageSize),
                RequestsCount = responceCount.Value
            };
            TempData["pageNumber"] = pageNumber;
            TempData["pageSize"] = pageSize;
            return View(requests);
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
            if (command is null)
            {
                ErrorVM error = new ErrorVM("ViewModel is null");
                return View("../Errors/ErrorPage", error);
            }
            if (command.Id == 0)
            {
                ErrorVM error = new ErrorVM("Id was not assigned");
                return View("../Errors/ErrorPage", error);
            }
            await _requestService.ChangeCalledStatusAsync(command);
            await GetAll(Convert.ToInt32(TempData["pageNumber"]), Convert.ToInt32(TempData["pageSize"]));
            return PartialView("GetAll");
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
            if (command is null)
            {
                ErrorVM error = new ErrorVM("ViewModel is null");
                return View("../Errors/ErrorPage", error);
            }
            if (command.Id == 0)
            {
                ErrorVM error = new ErrorVM("Id was not assigned");
                return View("../Errors/ErrorPage", error);
            }
            if (string.IsNullOrEmpty(command.ManagerComment))
            {
                ErrorVM error = new ErrorVM("ManagerComment must not be empty");
                return View("../Errors/ErrorPage", error);
            }
            await _requestService.ManagerCommentAsync(command);
            await GetAll(Convert.ToInt32(TempData["pageNumber"]), Convert.ToInt32(TempData["pageSize"]));
            return PartialView("GetAll");
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return View("../Errors/ErrorPage", error);
        }
    }
}

