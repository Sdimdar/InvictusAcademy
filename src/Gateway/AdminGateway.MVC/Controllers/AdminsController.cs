using AdminGateway.MVC.Models;
using AdminGateway.MVC.Models.DbModels;
using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using AutoMapper;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminGateway.MVC.Controllers;
[Route("AdminPanel/[controller]/[action]")]
public class AdminsController : Controller
{
    private readonly UserManager<AdminUser> _userManager;
    private readonly AdminDbContext _db;
    private readonly IAdminService _adminService;
    private readonly IMapper _mapper;


    public AdminsController(UserManager<AdminUser> userManager, AdminDbContext db, 
        IAdminService adminService, IMapper mapper)
    {
        _userManager = userManager;
        _db = db;
        _adminService = adminService;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminVm request, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _adminService.CreateNewAdmin(request, cancellationToken);
            return Ok(_mapper.Map<DefaultResponseObject<bool>>(response));
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
  
    }

    [HttpGet]
    public IActionResult EditProfile()
    {
        var users = _userManager.Users.ToList();

        users.Remove(users[0]);
        return Ok();
    }

    [HttpPost]
    public IActionResult EditProfile([FromBody] UserIdVm model)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.Id == model.UserId);
        if (user is not null)
        {
            if (user.Ban)
                user.Ban = false;
            else
                user.Ban = true;
            _db.SaveChanges();
            return Ok(user.Ban);
        }
        return BadRequest();

    }

}