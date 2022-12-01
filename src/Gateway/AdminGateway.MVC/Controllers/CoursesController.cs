﻿using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using AutoMapper;
using CommonStructures;
using Courses.Domain.Entities.CourseInfo;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using ServicesContracts.Jitsi;
using ServicesContracts.Jitsi.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;
[Route("AdminPanel/[controller]/[action]")]
public class CoursesController : Controller
{
    private readonly ICoursesService _coursesService;
    private readonly IStreamingRoomsService _streamingRoomsService;
    private readonly IMapper _mapper;

    public CoursesController(ICoursesService coursesService, IMapper mapper, IStreamingRoomsService streamingRoomsService)
    {
        _coursesService = coursesService;
        _mapper = mapper;
        _streamingRoomsService = streamingRoomsService;
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Создание курса",
        Description = "Необходимо передать в теле запроса данные по новому курсу"
    )]
    public async Task<ActionResult<DefaultResponseObject<CourseVm>>> CreateCourse([FromBody]CreateCourseAndRoomCommand request)
    {
        CreateCourseCommand courseCommand = _mapper.Map<CreateCourseCommand>(request);
        var response = await _coursesService.Create(courseCommand);
        if (response.IsSuccess)
        {
            CreateStreamingRoomCommand streamingRoomCommand = _mapper.Map<CreateStreamingRoomCommand>(request);
            var responseLast = await _streamingRoomsService.Create(streamingRoomCommand);
        }
        return Ok(response);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Редакитрование курса",
        Description = "Необходимо передать в теле запроса данные по редактируемому курсу + id")
    ]
    public async Task<ActionResult<DefaultResponseObject<CourseInfoDbModel>>> EditCourse([FromBody]EditCourseCommand request)
    {
        var response = await _coursesService.EditCourse(request);
        return Ok(response);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение курсов по типу",
        Description = "Необходимо передать в теле запроса данные об Id пользователя, а также тип запрашиваемых курсов. " +
                      "Что бы получить все активные курсы для неавторизованных, UserId не указывать, CourseType = 0"+
                      "Что бы получить все курсы для неавторизованных, UserId не указывать, CourseType = 4"
    )]
    public async Task<ActionResult<DefaultResponseObject<CourseInfoDbModel>>> GetCourses([FromQuery]GetCoursesQuery request)
    {
        var response = await _coursesService.GetCourses(request);
        return Ok(response);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение курса",
        Description = "Необходимо передать в теле запроса Id курса"
    )]
    public async Task<ActionResult<DefaultResponseObject<CourseInfoDbModel>>> GetCourse([FromQuery]GetCoursByIdQuery request)
    {
        var response = await _coursesService.GetCourse(request);
        return Ok(response);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Замена всех модулей в курсе на другой список",
        Description = "Необходимо передать в теле запроса Id курса и список Id добавляемых модулей")
    ]
    public async Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> ChangeAllModules([FromBody]ChangeAllModulesCommand request)
    {
        var response = await _coursesService.ChangeAllModules(request);
        return Ok(response);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Удаление курса",
        Description = "Необходимо передать в теле ID курса")
    ]
    public async Task<ActionResult<DefaultResponseObject<bool>>> DeleteCourse([FromBody]DeleteCourseCommand request)
    {
        var response = await _coursesService.Delete(request);
        return Ok(response);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение списка из ID модулей в курсе",
        Description = "Необходимо передать в строке запроса ID курса")
    ]
    public async Task<ActionResult<DefaultResponseObject<UniqueList<int>>>> GetCourseModulesId([FromQuery]GetCourseModulesIdQuerry request)
    {
        var response = await _coursesService.GetCourseModulesId(request);
        return Ok(response);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Добавление модуля в курс",
        Description = "Необходимо передать в теле запроса Id курса, Id добавляемого модуля" +
                      " и Index куда вставляется модуль, Если index < 0, то модуль добавится в конец")
    ]
    public async Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> InsertModule([FromBody]InsertModuleCommand request)
    {
        var response = await _coursesService.InsertModule(request);
        return Ok(response);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Добавление модулей в курс",
        Description = "Необходимо передать в теле запроса Id курса, список Id добавляемых в модуль" +
                      " и Index начиная с которого вставятся модуля, Если index < 0, то список добавится в конец")
    ]
    public async Task<ActionResult<DefaultResponseObject<List<CourseInfoVm>>>> InsertModules([FromBody]InsertModulesCommand request)
    {
        var response = await _coursesService.InsertModules(request);
        return Ok(response);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Удаление модуля из курса",
        Description = "Необходимо передать в теле запроса Id курса, Id удяляемого модуля")
    ]
    public async Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> RemoveModule([FromBody]RemoveModuleCommand request)
    {
        var response = await _coursesService.RemoveModule(request);
        return Ok(response);
    }
}