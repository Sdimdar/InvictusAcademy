using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using CommonStructures;
using Courses.Domain.Entities.CourseInfo;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminGateway.MVC.Controllers;
[Route("AdminPanel/[controller]/[action]")]
public class CoursesController : Controller
{
    private readonly ICoursesService _coursesService;

    public CoursesController(ICoursesService coursesService)
    {
        _coursesService = coursesService;
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Создание курса",
        Description = "Необходимо передать в теле запроса данные по новому курсу"
    )]
    public async Task<ActionResult<DefaultResponseObject<CourseVm>>> CreateCourse([FromBody]CreateCourseCommand request)
    {
        try
        {
            if (request is null)
            {
                ErrorVM error = new ErrorVM("Request is null");
                return Ok(error);
            }
            var response = await _coursesService.Create(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Редакитрование курса",
        Description = "Необходимо передать в теле запроса данные по редактируемому курсу + id")
    ]
    public async Task<ActionResult<DefaultResponseObject<CourseInfoDbModel>>> EditCourse([FromBody]EditCourseCommand request)
    {
        try
        {
            if (request is null)
            {
                ErrorVM error = new ErrorVM("Request is null");
                return Ok(error);
            }
            if (request.Id <= 0)
            {
                ErrorVM error = new ErrorVM("Id was not assigned");
                return Ok(error);
            }
            var response = await _coursesService.EditCourse(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Редактирует модель курс",
        Description = "Необходимо передать Id и заполнить EditCourseCommand")
    ]
    public async Task<ActionResult<DefaultResponseObject<CourseInfoDbModel>>> GetCourses([FromQuery]GetCoursesQuery request)
    {
        try
        {
            if (request is null)
            {
                ErrorVM error = new ErrorVM("Request is null");
                return Ok(error);
            }
            if (request.UserId <= 0)
            {
                ErrorVM error = new ErrorVM("UserId was not assigned");
                return Ok(error);
            }
            if (request.Type < 0)
            {
                ErrorVM error = new ErrorVM("Courses Type was not assigned");
                return Ok(error);
            }
            var response = await _coursesService.GetCourses(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Замена всех модулей в курсе на другой список",
        Description = "Необходимо передать в теле запроса Id курса и список Id добавляемых модулей")
    ]
    public async Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> ChangeAllModules([FromBody]ChangeAllModulesCommand request)
    {
        try
        {
            if (request is null)
            {
                ErrorVM error = new ErrorVM("Request is null");
                return Ok(error);
            }
            if (request.CourseId <= 0)
            {
                ErrorVM error = new ErrorVM("Id was not assigned");
                return Ok(error);
            }
            if (request.ModulesId is null)
            {
                ErrorVM error = new ErrorVM("Modules is not selected");
                return Ok(error);
            }
            var response = await _coursesService.ChangeAllModules(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Удаление курса",
        Description = "Необходимо передать в теле ID курса")
    ]
    public async Task<ActionResult<DefaultResponseObject<bool>>> DeleteCourse([FromBody]DeleteCourseCommand request)
    {
        try
        {
            if (request is null)
            {
                ErrorVM error = new ErrorVM("Request is null");
                return Ok(error);
            }
            if (request.Id <= 0)
            {
                ErrorVM error = new ErrorVM("Id was not assigned");
                return Ok(error);
            }
            var response = await _coursesService.Delete(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение списка из ID модулей в курсе",
        Description = "Необходимо передать в строке запроса ID курса")
    ]
    public async Task<ActionResult<DefaultResponseObject<UniqueList<int>>>> GetCourseModulesId([FromQuery]GetCourseModulesIdQuerry request)
    {
        try
        {
            if (request is null)
            {
                ErrorVM error = new ErrorVM("Request is null");
                return Ok(error);
            }
            if (request.CourseId <= 0)
            {
                ErrorVM error = new ErrorVM("CourseId was not assigned");
                return Ok(error);
            }
            var response = await _coursesService.GetCourseModulesId(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Добавление модуля в курс",
        Description = "Необходимо передать в теле запроса Id курса, Id добавляемого модуля" +
                      " и Index куда вставляется модуль, Если index < 0, то модуль добавится в конец")
    ]
    public async Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> InsertModule([FromBody]InsertModuleCommand request)
    {
        try
        {
            if (request is null)
            {
                ErrorVM error = new ErrorVM("Request is null");
                return Ok(error);
            }
            if (request.CourseId <= 0)
            {
                ErrorVM error = new ErrorVM("CourseId was not assigned");
                return Ok(error);
            }
            if (request.ModuleId <= 0)
            {
                ErrorVM error = new ErrorVM("ModuleId was not assigned");
                return Ok(error);
            }
            if (request.Index <= 0)
            {
                ErrorVM error = new ErrorVM("Index was not assigned");
                return Ok(error);
            }
            var response = await _coursesService.InsertModule(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Добавление модулей в курс",
        Description = "Необходимо передать в теле запроса Id курса, список Id добавляемых в модуль" +
                      " и Index начиная с которого вставятся модуля, Если index < 0, то список добавится в конец")
    ]
    public async Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> InsertModules([FromBody]InsertModulesCommand request)
    {
        try
        {
            if (request is null)
            {
                ErrorVM error = new ErrorVM("Request is null");
                return Ok(error);
            }
            if (request.CourseId <= 0)
            {
                ErrorVM error = new ErrorVM("CourseId was not assigned");
                return Ok(error);
            }
            if (request.ModulesId is null)
            {
                ErrorVM error = new ErrorVM("ModulesId is null");
                return Ok(error);
            }
            if (request.StartIndex <= 0)
            {
                ErrorVM error = new ErrorVM("StartIndex was not assigned");
                return Ok(error);
            }
            var response = await _coursesService.InsertModules(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Удаление модуля из курса",
        Description = "Необходимо передать в теле запроса Id курса, Id удяляемого модуля")
    ]
    public async Task<ActionResult<DefaultResponseObject<CourseInfoVm>>> RemoveModule([FromBody]RemoveModuleCommand request)
    {
        try
        {
            if (request is null)
            {
                ErrorVM error = new ErrorVM("Request is null");
                return Ok(error);
            }
            if (request.CourseId <= 0)
            {
                ErrorVM error = new ErrorVM("CourseId was not assigned");
                return Ok(error);
            }
            if (request.ModuleId <= 0)
            {
                ErrorVM error = new ErrorVM("ModuleID was not assigned");
                return Ok(error);
            }
            var response = await _coursesService.RemoveModule(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            ErrorVM error = new ErrorVM(e.Message);
            return Ok(error);
        }
    }
}