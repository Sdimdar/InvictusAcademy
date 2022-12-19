using AdminGateway.MVC.Services.Interfaces;
using CommonStructures;
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
    private readonly ILogger<CloudStorageController> _logger;

    public CloudStorageController(ICloudStorages cloudStorages, ILogger<CloudStorageController> logger)
    {
        _cloudStorages = cloudStorages;
        _logger = logger;
    }
    [HttpGet]
    [SwaggerOperation(
        Summary = "Возвращает список запросов постранично, если передать страницу 0, вернет всех",
        Description = "Необходимо передать номер страницы и количество на странице")
    ]
    public async Task<ActionResult<DefaultResponseObject<GetAllFilesVM>>> GetAllFiles(int pageNumber = 1, int pageSize = 10)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}: " + $"pageNumbe" + $"r: {pageNumber}" + $"pageSize: {pageSize}");
        var response = await _cloudStorages.GetFilesAsync(pageNumber, pageSize);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}: " +
                               $"IsSucces: {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Files Count: {response.Value.Files.Count}" +
                               $"pageSize: {response.Value.PageSize}" +
                               $"pageNumber: {response.Value.PageNumber}");
        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Возвращает количество записей Request , для пагинации")
    ]
    public async Task<ActionResult<DefaultResponseObject<int>>> GetFilesCount()
    {
        var response = await _cloudStorages.GetFilesCount();
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"isSucces: {response.IsSuccess}" + $"Count:" + $" {response.Value}");
        return Ok(response);
    }
}