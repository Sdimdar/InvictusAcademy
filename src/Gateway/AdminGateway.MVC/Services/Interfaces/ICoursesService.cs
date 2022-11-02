using Courses.Domain.Entities;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Commands;
using ServicesContracts.Courses.Requests.Querries;
using ServicesContracts.Courses.Responses;

namespace AdminGateway.MVC.Services.Interfaces;

public interface ICoursesService
{
    Task<DefaultResponseObject<CourseInfoDbModel>> Create(CreateCourseCommand request);
    Task<DefaultResponseObject<CourseInfoDbModel>> EditCourse(EditCourseCommand request);
    Task<DefaultResponseObject<CoursesVm>> GetCourses([FromQuery] GetCoursesQuery request);
}