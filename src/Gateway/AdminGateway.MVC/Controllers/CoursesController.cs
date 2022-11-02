using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using Courses.Domain.Entities;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Commands;
using ServicesContracts.Courses.Requests.Querries;
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
    public async Task<ActionResult<DefaultResponseObject<CourseInfoDbModel>>> CreateCourse([FromBody]CreateCourseCommand request)
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
}