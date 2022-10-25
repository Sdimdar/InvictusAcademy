using AdminGateway.MVC.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdminGateway.MVC.Controllers;

public class UsersController : Controller
{
    private readonly IGetUsers _iGetUsers;
    private readonly IMapper _mapper;

    public UsersController(IGetUsers iGetUsers, IMapper mapper)
    {
        _iGetUsers = iGetUsers;
        _mapper = mapper;
    }
    // GET
    public async Task<IActionResult> GetAllRegisteredUsers()
    {
        var response = await _iGetUsers.GetUsersAsync();
        var usersList = response.Value;
        return Ok(usersList?.Users);
    }
}