using Admin.MVC.Services.Interfaces;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Request.Requests.Commands;
using ServicesContracts.Request.Responses;

namespace Admin.MVC.Controllers;

public class RequestsController : Controller
{
    private readonly IRequestService _requestService;

    public RequestsController(IRequestService requestService)
    {
        _requestService = requestService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 5)
    {
        var responce = await _requestService.GetAllRequestsAsync(pageNumber, pageSize);
        var responceCount = await _requestService.GetRequestsCountAsync();
        GetAllRequestVm requests = new GetAllRequestVm
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Requests = responce.Value.Requests,
            TotalPages = (int)Math.Ceiling(responceCount.Value / (double)pageSize)
        };
        TempData["pageNumber"] = pageNumber;
        TempData["pageSize"] = pageSize;
        return View(requests);
    }

    [HttpPost]
    public async Task<IActionResult> ChangeCalled(ChangeCalledStatusCommand command)
    {
        await _requestService.ChangeCalledStatusAsync(command);
        await GetAll(Convert.ToInt32(TempData["pageNumber"]), Convert.ToInt32(TempData["pageSize"]));
        return PartialView("GetAll");
    }

    [HttpPost]
    public async Task<IActionResult> ManagerComment(ManagerCommentCommand command)
    {
        await _requestService.ManagerCommentAsync(command);
        await GetAll(Convert.ToInt32(TempData["pageNumber"]), Convert.ToInt32(TempData["pageSize"]));
        return PartialView("GetAll");
    }
}

