using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Requests.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;
[Route("AdminPanel/[controller]/[action]")]
public class UsersController : Controller
{
    private readonly IGetUsers _iGetUsers;

    public UsersController(IGetUsers iGetUsers)
    {
        _iGetUsers = iGetUsers;
    }
    [HttpGet]
    [SwaggerOperation(
        Summary = "Возвращает список запросов постранично, если передать страницу 0, вернет всех",
        Description = "Необходимо передать номер страницы и количество на странице")
    ]
    public async Task<IActionResult> GetAllRegisteredUsers([FromQuery]int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var response = await _iGetUsers.GetUsersAsync(pageNumber, pageSize);
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
        Summary = "Возвращает количество зарегистрированных пользователей , для пагинации")
    ]
    public async Task<ActionResult<DefaultResponseObject<int>>> GetUsersCount()
    {
        try
        {
            var response = await _iGetUsers.GetUsersCountAsync();
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
        Summary = "Изменяет статус для User бан/не бан",
        Description = "Необходимо передать id")
    ]
    public async Task<ActionResult<DefaultResponseObject<string>>> ToBan([FromQuery]ToBanCommand command)
    {
        try
        {
            if (command.Id <= 0)
            {
                ErrorVM error = new ErrorVM("Id was not assigned");
                return View("../Errors/ErrorPage", error);
            }
            var response = await _iGetUsers.ChangeBanStatusAsync(command);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return View("../Errors/ErrorPage", error);
        }
    }
    
}