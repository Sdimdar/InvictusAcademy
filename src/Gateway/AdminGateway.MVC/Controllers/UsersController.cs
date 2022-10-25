using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using AutoMapper;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> GetAllRegisteredUsers(int pageNumber = 1, int pageSize = 10)
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
        //var response = await _iGetUsers.GetUsersAsync();
        //var usersList = response.Value;

        //return View(_mapper.Map<List<RegisteredUserVM>>(usersList.Users));
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
    
}