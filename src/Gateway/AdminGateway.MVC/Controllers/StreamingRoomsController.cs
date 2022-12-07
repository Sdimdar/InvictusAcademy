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

    public StreamingRoomsController(IStreamingRoomService streamingRoomService, IMapper mapper)
    {
        _streamingRoomService = streamingRoomService;
        _mapper = mapper;
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Создание стриминговый комнаты",
        Description = "Необходимо передать в теле запроса id курса"
    )]
    public async Task<ActionResult<DefaultResponseObject<string>>> Create([FromBody]CreateStreamingRoomCommand request)
    {
        var response = await _streamingRoomService.Create(request);
        return Ok(_mapper.Map<DefaultResponseObject<string>>(response));
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Закрытие/Открытие стриминговый комнаты",
        Description = "Необходимо передать в теле запроса id курса"
    )]
    public async Task<ActionResult<DefaultResponseObject<string>>> OpenOrCloseRoom([FromBody]string address)
    {
        var response = await _streamingRoomService.OpenOrCloseRoom(address);
        return Ok(_mapper.Map<DefaultResponseObject<string>>(response));
    }
    
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Взять все стриминговые комнаты",
        Description = "Необходимо передать в строке номер страницы и кол-во элементов на странице"
    )]
    public async Task<ActionResult<DefaultResponseObject<AllStreamingRoomsVm>>> GetAll([FromQuery]GetAllRoomsQuery request)
    {
        var response = await _streamingRoomService.GetAll(request);
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
        return Ok(_mapper.Map<DefaultResponseObject<int>>(response));
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получить стрим комнату",
        Description = "Необходимо передать в строке запроса адрес комнаты"
    )]
    public async Task<ActionResult<DefaultResponseObject<StreamingRoomVm>>> GetByAddress([FromBody]GetByAddressQuery request)
    {
        var response = await _streamingRoomService.GetByAddress(request);
        return Ok(_mapper.Map<DefaultResponseObject<AllStreamingRoomsVm>>(response));
    }
}