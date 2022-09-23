using InvictusAcademyApp.Enums;
using InvictusAcademyApp.Models.RequestModels;
using InvictusAcademyApp.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAcademyApp.Controllers;

[ApiController]
[Route("Account/[action]")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
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
    public async Task<IActionResult> LogOff()
    {
        await _accountService.LogOf();
        return Ok();
    }
}