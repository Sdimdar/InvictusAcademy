using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using Ardalis.Result;
using AutoMapper;
using CommonStructures;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Responses;
using ServicesContracts.Jitsi;
using ServicesContracts.Jitsi.Commands;
using ServicesContracts.Jitsi.Models;
using ServicesContracts.Jitsi.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;
[Route("AdminPanel/[controller]/[action]")]
public class StreamingRoomsController : Controller
{
    private readonly IStreamingRoomService _streamingRoomService;
    private readonly IMapper _mapper;
    private readonly ILogger<StreamingRoomsController> _logger;

    public StreamingRoomsController(IStreamingRoomService streamingRoomService, IMapper mapper, ILogger<StreamingRoomsController> logger)
    {
        _streamingRoomService = streamingRoomService;
        _mapper = mapper;
        _logger = logger;
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Создание стриминговый комнаты",
        Description = "Необходимо передать в теле запроса id курса"
    )]
    public async Task<ActionResult<DefaultResponseObject<string>>> Create([FromBody]CreateStreamingRoomCommand request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"Name {request.Name}" + $"ImageLink {request.ImageLink}");
        var response = await _streamingRoomService.Create(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"isSucces {response.IsSuccess}" +
                               $"");
        return Ok(_mapper.Map<DefaultResponseObject<string>>(response));
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Закрытие/Открытие стриминговый комнаты",
        Description = "Необходимо передать в теле запроса id курса"
    )]
    public async Task<ActionResult<DefaultResponseObject<string>>> OpenOrCloseRoom([FromBody]string address)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"Address {address}");
        var response = await _streamingRoomService.OpenOrCloseRoom(address);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"");
        return Ok(_mapper.Map<DefaultResponseObject<string>>(response));
    }
    
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Взять все стриминговые комнаты",
        Description = "Необходимо передать в строке номер страницы и кол-во элементов на странице"
    )]
    public async Task<ActionResult<DefaultResponseObject<AllStreamingRoomsVm>>> GetAll([FromQuery]GetAllRoomsQuery request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"PageNumber {request.PageNumber}" + $"PageSize {request.PageSize}");
        var response = await _streamingRoomService.GetAll(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"PageNumber {response.Value.PageNumber}" +
                               $"PageSize {response.Value.PageSize}" +
                               $"Filter {response.Value.Filter}" +
                               $"");
        return Ok(_mapper.Map<DefaultResponseObject<AllStreamingRoomsVm>>(response));
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Закрытие/Открытие стриминговый комнаты",
        Description = ""
    )]
    public async Task<ActionResult<DefaultResponseObject<int>>> GetCount()
    {
        var response = await _streamingRoomService.GetCount();
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" + 
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"isSucces {response.IsSuccess}" + $"Count {response.Value}" + $"");
        return Ok(_mapper.Map<DefaultResponseObject<int>>(response));
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получить стрим комнату",
        Description = "Необходимо передать в строке запроса адрес комнаты"
    )]
    public async Task<ActionResult<DefaultResponseObject<StreamingRoomVm>>> GetByAddress([FromBody]GetByAddressQuery request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"Address {request.Address}");
        var response = await _streamingRoomService.GetByAddress(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Address {response.Value.Address}" +
                               $"Name {response.Value.Name}" +
                               $"ImageLink {response.Value.ImageLink}" +
                               $"IsOpened {response.Value.IsOpened}" +
                               $"");
        return Ok(_mapper.Map<DefaultResponseObject<AllStreamingRoomsVm>>(response));
    }
}