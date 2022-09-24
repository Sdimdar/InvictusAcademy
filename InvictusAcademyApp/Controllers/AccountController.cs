using InvictusAcademyApp.Enums;
using InvictusAcademyApp.Models.DbModels;
using InvictusAcademyApp.Models.RequestModels;
using InvictusAcademyApp.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAcademyApp.Controllers;

[ApiController]
[Route("Account/[action]")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly UserManager<User> _userManager;

    public AccountController(IAccountService accountService, UserManager<User> userManager)
    {
        _accountService = accountService;
        _userManager = userManager;
    }
    
    [HttpPost]
    [ActionName("Register")]
    public async Task<ActionResult<DefaultResponse>> Register(RegisterRequestModel model)
    {
        try
        {
            if (model is null)
            {
                return BadRequest(new DefaultResponse
                {
                    ResponseStatus = ResponseStatusType.NotFound
                });
            }
            var result = await _accountService.Register(model);
            if (result.ResponseStatus == ResponseStatusType.Ok)
            {
                return Ok(new DefaultResponse
                {
                    ResponseStatus = ResponseStatusType.Ok
                });
            }
            return BadRequest(new DefaultResponse
            {
                ResponseStatus = ResponseStatusType.Error
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { errorMessage = e.Message });
        }
    }

    [HttpPost]
    [ActionName("Login")]
    public async Task<ActionResult<DefaultResponse>> Login(LoginRequestModel model)
    {
        try
        {
            if (model is null)
            {
                return BadRequest(new DefaultResponse
                {
                    ResponseStatus = ResponseStatusType.NotFound
                });
            }
           var result = await _accountService.LogIn(model);
           if (result.ResponseStatus == ResponseStatusType.Ok)
           {
               return Ok(new DefaultResponse
               {
                   ResponseStatus = ResponseStatusType.Ok
               });
           }
           return BadRequest(new DefaultResponse
           {
               ResponseStatus = ResponseStatusType.Error
           });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { errorMessage = e.Message });
        }
    }
    
    [Authorize]
    [HttpPost]
    [ActionName("LogOff")]
    public async Task<IActionResult> LogOff()
    {
        await _accountService.LogOf();
        return Ok();
    }

    [Authorize]
    [HttpGet]
    [ActionName("GetUserInfo")]
    public async Task<ActionResult<DefaultResponse>> GetUserInfo()
    {
        try
        {
            User user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user is null)
                return BadRequest(new DefaultResponse(responseStatus: ResponseStatusType.NotFound));
            GetUserInfoResponse response = new GetUserInfoResponse
            {
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                InstagramLink = user.InstagramLink,
                Citizenship = user.Citizenship
            };
            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { errorMessage = e.Message });
        }
    }
}