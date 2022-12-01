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
            newStreamingRoom.IsOpened = false;
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
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Закрытие/Открытие стриминговый комнаты",
        Description = "Необходимо передать в теле запроса id курса"
    )]
    public async Task<ActionResult<DefaultResponseObject<string>>> OpenOrCloseRoom(int id, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            StreamingRoomDbModel? streamingRoom = await _streamingRoomRepository.GetFirstOrDefaultAsync(s=>s.Id == id);
            if (streamingRoom is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Streaming Room not found with this id: {id}");
                return BadRequest($"{BussinesErrors.NotFound.ToString()}: Streaming Room not found with this id: {id}");
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
    public async Task<ActionResult<DefaultResponseObject<AllStreamingRoomsVm>>> GetAll(int pageNumber, int pageSize , CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            var roomsCount = _streamingRoomRepository.GetCountAsync();
            if (pageSize == 0)
            {
                pageNumber = 1;
                pageSize = await roomsCount;
            }

            if (await roomsCount == 0)
            {
                _logger.LogWarning($"{BussinesErrors.ListIsEmpty.ToString()}: rooms list is empty");
                return BadRequest($"{BussinesErrors.ListIsEmpty.ToString()}: rooms list is empty");
            }

            var data = await _streamingRoomRepository.GetFilteredBatchOfData(pageSize, pageNumber);

            var allStreamingRoomsVm = new AllStreamingRoomsVm()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
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
        Summary = "Закрытие/Открытие стриминговый комнаты",
        Description = "Необходимо передать в теле запроса id курса"
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
}