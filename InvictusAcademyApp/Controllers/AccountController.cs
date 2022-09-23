﻿using InvictusAcademyApp.Enums;
using InvictusAcademyApp.Models.RequestModels;
using InvictusAcademyApp.Services.Abstractions;
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
            await _accountService.Register(model);
            return Ok(new DefaultResponse
            {
                ResponseStatus = ResponseStatusType.Ok
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { errorMessage = e.Message });
        }
    }
}