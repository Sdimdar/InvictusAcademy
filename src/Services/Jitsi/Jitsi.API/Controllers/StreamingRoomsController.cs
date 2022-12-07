using Ardalis.Result;
using AutoMapper;
using CommonStructures;
using DataTransferLib.Models;
using Jitsi.API.Models.DbModels;
using Jitsi.API.Repostories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Jitsi;
using ServicesContracts.Jitsi.Commands;
using ServicesContracts.Jitsi.Models;
using ServicesContracts.Jitsi.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Jitsi.API.Controllers;
[Route("AdminPanel/[controller]/[action]")]
public class StreamingRoomsController : Controller
{
    private readonly IStreamingRoomRepository _streamingRoomRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<StreamingRoomsController> _logger;

    public StreamingRoomsController(IStreamingRoomRepository streamingRoomRepository, ILogger<StreamingRoomsController> logger, IMapper mapper)
    {
        _streamingRoomRepository = streamingRoomRepository;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Создание стриминговый комнаты",
        Description = "Необходимо передать в теле запроса id курса"
    )]
    public async Task<ActionResult<DefaultResponseObject<string>>> Create([FromBody]CreateStreamingRoomCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            StreamingRoomDbModel newStreamingRoom = _mapper.Map<StreamingRoomDbModel>(request);
            newStreamingRoom.IsOpened = true;
            newStreamingRoom.Address = Guid.NewGuid().ToString();
            var createdStreamingRoom = await _streamingRoomRepository.AddAsync(newStreamingRoom);
            var response = Result.Success(createdStreamingRoom);
            return Ok(_mapper.Map<DefaultResponseObject<string>>(response));
        }
        catch (Exception e)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
            return BadRequest($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
        }
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Закрытие/Открытие стриминговый комнаты",
        Description = "Необходимо передать в теле запроса id курса"
    )]
    public async Task<ActionResult<DefaultResponseObject<string>>> OpenOrCloseRoom([FromBody]string address, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            var streamingRoom = await _streamingRoomRepository.GetFirstOrDefaultAsync(p=>p.Address==address);
            if (streamingRoom is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Room with this address {address} not found");
                return BadRequest($"{BussinesErrors.NotFound.ToString()}: Room with this address {address} not found");
            }
            if (streamingRoom.IsOpened)
            {
                streamingRoom.IsOpened = false;
            }
            else
            {
                streamingRoom.IsOpened = true;
            }

            await _streamingRoomRepository.UpdateAsync(streamingRoom);
            return Ok(_mapper.Map<DefaultResponseObject<string>>(Result.Success()));
        }
        catch (Exception e)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
            return BadRequest($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
        }
    }
    
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Взять все стриминговые комнаты",
        Description = "Необходимо передать в строке номер страницы и кол-во элементов на странице"
    )]
    public async Task<ActionResult<DefaultResponseObject<AllStreamingRoomsVm>>> GetAll([FromQuery]GetAllRoomsQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            var roomsCount = _streamingRoomRepository.GetCountAsync();
            if (request.PageSize == 0)
            {
                request.PageNumber = 1;
                request.PageSize = await roomsCount;
            }

            if (await roomsCount == 0)
            {
                _logger.LogWarning($"{BussinesErrors.ListIsEmpty.ToString()}: rooms list is empty");
                return BadRequest($"{BussinesErrors.ListIsEmpty.ToString()}: rooms list is empty");
            }

            var data = await _streamingRoomRepository.GetFilteredBatchOfData(request.PageSize, request.PageNumber);

            var allStreamingRoomsVm = new AllStreamingRoomsVm()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                StreamingRooms = _mapper.Map<List<StreamingRoomVm>>(data)
            };
            var response = Result.Success(allStreamingRoomsVm);
            return Ok(_mapper.Map<DefaultResponseObject<AllStreamingRoomsVm>>(response));
        }
        catch (Exception e)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
            return BadRequest($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
        }
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получить количество открытых комнат",
        Description = ""
    )]
    public async Task<ActionResult<DefaultResponseObject<int>>> GetCount(CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            var roomsCount = await _streamingRoomRepository.GetCountAsync();
            return Ok(_mapper.Map<DefaultResponseObject<int>>(Result.Success(roomsCount)));
        }
        catch (Exception e)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
            return BadRequest($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
        }
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получить стрим комнату",
        Description = "Необходимо передать в строке запроса адрес комнаты"
    )]
    public async Task<ActionResult<DefaultResponseObject<StreamingRoomVm>>> GetByAddress([FromQuery] GetByAddressQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            var room = await _streamingRoomRepository.GetFirstOrDefaultAsync(p=>p.Address==request.Address);
            if (room is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Room with this address {request.Address} not found");
                return BadRequest($"{BussinesErrors.NotFound.ToString()}: Room with this address {request.Address} not found");
            }
            return Ok(_mapper.Map<DefaultResponseObject<StreamingRoomVm>>(Result.Success(room)));
        }
        catch (Exception e)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
            return BadRequest($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
        }
    }
}