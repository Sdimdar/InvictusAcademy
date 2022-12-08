using AdminGateway.MVC.Services.Interfaces;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.CloudStorage.Requests.Commands;
using ServicesContracts.CloudStorage.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;
[Route("AdminPanel/[controller]/[action]")]
public class CloudStorageController : Controller
{
    private readonly ICloudStorages _cloudStorages;

    public CloudStorageController(ICloudStorages cloudStorages)
    {
        _cloudStorages = cloudStorages;
    }
    [HttpGet]
    [SwaggerOperation(
        Summary = "Возвращает список запросов постранично, если передать страницу 0, вернет всех",
        Description = "Необходимо передать номер страницы и количество на странице")
    ]
    public async Task<ActionResult<DefaultResponseObject<GetAllFilesVM>>> GetAllFiles(int pageNumber = 1, int pageSize = 10)
    {
        var response = await _cloudStorages.GetFilesAsync(pageNumber, pageSize);
        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Возвращает количество записей Request , для пагинации")
    ]
    public async Task<ActionResult<DefaultResponseObject<int>>> GetFilesCount()
    {
        var response = await _cloudStorages.GetFilesCount();
        return Ok(response);
    }
}