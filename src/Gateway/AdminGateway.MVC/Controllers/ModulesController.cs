using AdminGateway.MVC.Models.DbModels;
using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using AutoMapper;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;
[Route("AdminPanel/[controller]/[action]")]

public class ModulesController : Controller
{
    private readonly IModuleService _moduleService;
    private readonly IMapper _mapper;

    public ModulesController(IModuleService moduleService, IMapper mapper)
    {
        _moduleService = moduleService;
        _mapper = mapper;
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Создание модуля",
        Description = "Для создания модуля нужно передать его название и описание, также можно сразу передать вместе с статьями",
        Tags = new[] { "Module" })
    ]
    public async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> CreateModule([FromBody]CreateModuleVM request, 
        CancellationToken cancellationToken = default)
    { 
        try
        {
            var response = await _moduleService.CreateNewModule(request, cancellationToken);
            return Ok(_mapper.Map<DefaultResponseObject<ModuleInfoVm>>(response));
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
}