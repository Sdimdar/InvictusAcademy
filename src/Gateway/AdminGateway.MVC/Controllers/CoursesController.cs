using AdminGateway.MVC.Models;
using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using AutoMapper;
using CommonStructures;
using Courses.Domain.Entities.CourseInfo;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using ServicesContracts.Jitsi;
using ServicesContracts.Jitsi.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;
[Authorize(Roles = $"{RolesHelper.Administrator},{RolesHelper.Instructor}")]
[Route("AdminPanel/[controller]/[action]")]
public class CoursesController : Controller
{
    private readonly ICoursesService _coursesService;
    private readonly IMapper _mapper;
    private readonly ILogger<CoursesController> _logger;

    public CoursesController(ICoursesService coursesService, IMapper mapper, ILogger<CoursesController> logger)
    {
        _coursesService = coursesService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Создание курса",
        Description = "Необходимо передать в теле запроса данные по новому курсу"
    )]
    public async Task<ActionResult<DefaultResponseObject<CourseVm>>> CreateCourse([FromBody] CreateCourseCommand request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}:" +
                               $"Name {request.Name}" + $"Descripti" + $"on {request.Description}" + $"Cost {request.Cost}" + $"VideoLink {request.VideoLink}" + $"PassingDayCount {request.PassingDayCount}" + $"SecondName {request.SecondName}" + $"SecondDescription {request.SecondDescription}");
        CreateCourseCommand courseCommand = _mapper.Map<CreateCourseCommand>(request);
        var response = await _coursesService.Create(courseCommand);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces: {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Id {response.Value.Id}" +
                               $"Name {response.Value.Name}" +
                               $"Purchased {response.Value.Purchased}" +
                               $"Cost {response.Value.Cost}" +
                               $"Description {response.Value.Description}" +
                               $"VideoLink {response.Value.VideoLink}");
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Редакитрование курса",
        Description = "Необходимо передать в теле запроса данные по редактируемому курсу + id")
    ]
    public async Task<ActionResult<DefaultResponseObject<CourseInfoDbModel>>> EditCourse([FromBody] EditCourseCommand request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}:" +
                               $"Id {request.Id}" +
                               $"Name {request.Name}" + $"IsActive {request.IsActive}" + $"VideoLink {request.VideoLink}" + $"PassingDayCount {request.PassingDayCount}" + $"Description {request.Description}");
        var response = await _coursesService.EditCourse(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Id {response.Value.Id}" +
                               $"ModulesId {response.Value.ModulesId}" +
                               $"ModulesString {response.Value.ModulesString}");
        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение курсов по типу",
        Description = "Необходимо передать в теле запроса данные об Id пользователя, а также тип запрашиваемых курсов. " +
                      "Что бы получить все активные курсы для неавторизованных, UserId не указывать, CourseType = 0" +
                      "Что бы получить все курсы для неавторизованных, UserId не указывать, CourseType = 4"
    )]
    public async Task<ActionResult<DefaultResponseObject<CourseInfoDbModel>>> GetCourses([FromQuery] GetCoursesQuery request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}:" + $"Type {request.Type}" + $"UserId {request.UserId}");
        var response = await _coursesService.GetCourses(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Courses Count {response.Value.Courses.Count}");
        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение курса",
        Description = "Необходимо передать в теле запроса Id курса"
    )]
    public async Task<ActionResult<DefaultResponseObject<CourseInfoDbModel>>> GetCourse([FromQuery] GetCourseByIdQuery request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}:" +
                               $"Id {request.Id}");
        var response = await _coursesService.GetCourse(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Id {response.Value.Id}" +
                               $"Name {response.Value.Name}" +
                               $"Cost {response.Value.Cost}" +
                               $"Description {response.Value.Description}" +
                               $"IsActive {response.Value.IsActive}");
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Замена всех модулей в курсе на другой список",
        Description = "Необходимо передать в теле запроса Id курса и список Id добавляемых модулей")
    ]
    public async Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> ChangeAllModules([FromBody] ChangeAllModulesCommand request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}:" + $"CourseId {request.CourseId}" + $"ModulesId {request.ModulesId}");
        var response = await _coursesService.ChangeAllModules(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"CourseData {response.Value.CourseData}" +
                               $"ModulesId {response.Value.ModulesId}");
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Удаление курса",
        Description = "Необходимо передать в теле ID курса")
    ]
    public async Task<ActionResult<DefaultResponseObject<bool>>> DeleteCourse([FromBody] DeleteCourseCommand request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}:" + $"Id {request.Id}");
        var response = await _coursesService.Delete(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"isSucces {response.IsSuccess}");
        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение списка из ID модулей в курсе",
        Description = "Необходимо передать в строке запроса ID курса")
    ]
    public async Task<ActionResult<DefaultResponseObject<UniqueList<int>>>> GetCourseModulesId([FromQuery] GetCourseModulesIdQuerry request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}:" + $"CourseId {request.CourseId}");
        var response = await _coursesService.GetCourseModulesId(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"Count {response.Value.Count}");
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Добавление модуля в курс",
        Description = "Необходимо передать в теле запроса Id курса, Id добавляемого модуля" +
                      " и Index куда вставляется модуль, Если index < 0, то модуль добавится в конец")
    ]
    public async Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> InsertModule([FromBody] InsertModuleCommand request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" + $"Index {request.Index}" + $"CourseId {request.CourseId}" + $"ModuleId {request.ModuleId}");
        var response = await _coursesService.InsertModule(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"ModulesId {response.Value.ModulesId}" +
                               $"CourseData Name {response.Value.CourseData.Name}" +
                               $"CourseData Id {response.Value.CourseData.Id}" +
                               $"");
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Добавление модулей в курс",
        Description = "Необходимо передать в теле запроса Id курса, список Id добавляемых в модуль" +
                      " и Index начиная с которого вставятся модуля, Если index < 0, то список добавится в конец")
    ]
    public async Task<ActionResult<DefaultResponseObject<List<CourseInfoVm>>>> InsertModules([FromBody] InsertModulesCommand request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}:" + $"CourseId {request.CourseId}" + $"ModulesId {request.ModulesId}" + $"ModulesId {request.ModulesId}");
        var response = await _coursesService.InsertModules(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"ModulesId {response.Value.ModulesId}" +
                               $"CourseData Name {response.Value.CourseData.Name}" +
                               $"CourseData Id {response.Value.CourseData.Id}" +
                               $"");
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Удаление модуля из курса",
        Description = "Необходимо передать в теле запроса Id курса, Id удяляемого модуля")
    ]
    public async Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> RemoveModule([FromBody] RemoveModuleCommand request)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}:" + $"CourseId {request.CourseId}" + $"ModuleId {request.ModuleId}");
        var response = await _coursesService.RemoveModule(request);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}:" +
                               $"isSucces {response.IsSuccess}" +
                               $"ValidationErrors: {response.ValidationErrors}" +
                               $"Errors: {response.Errors}" +
                               $"ModulesId {response.Value.ModulesId}" +
                               $"CourseData Name {response.Value.CourseData.Name}" +
                               $"CourseData Id {response.Value.CourseData.Id}" +
                               $"");
        return Ok(response);
    }
}