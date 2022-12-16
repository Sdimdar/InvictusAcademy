using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.CloudStorage.Requests.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;

public class CloudStorageController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CloudStorageController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)] 
    [DisableRequestSizeLimit]
    [HttpPost("/CloudStorage/UploadFile")]
    [SwaggerOperation(
        Summary = "Загрузка файла",
        Description = "Необходимо выбрать файл для загрузки на сервер",
        Tags = new[] { "S3" })
    ]
    public async Task<ActionResult<DefaultResponseObject<string>>> UploadFile(CancellationToken cancellationToken)
    {
        IFormFileCollection formFileCollection = Request.Form.Files;
        IFormFile formFile = formFileCollection![0];
        string filePath = String.Empty;
        if (formFile.Length > 0) {
            filePath = Path.Combine(Directory.GetCurrentDirectory(), formFile.FileName);
            await using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await formFile.CopyToAsync(fileStream, cancellationToken);
        }
        
        var response = await _mediator.Send(new UploadFileCommand{FilePath = filePath} , cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<string>>(response));
    }
}