using Ardalis.ApiEndpoints;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Querries;
using ServicesContracts.Courses.Responses;

namespace Courses.API.Endpoints.Course
{
    public class GetCourses : EndpointBaseAsync
        .WithRequest<GetCoursesQuerry>
        .WithActionResult<DefaultResponseObject<CoursesVm>>
    {
        [HttpGet("/Courses/GetCourses")]
        public async override Task<ActionResult<DefaultResponseObject<CoursesVm>>> HandleAsync([FromBody] GetCoursesQuerry request,
                                                                                               CancellationToken cancellationToken = default)
        {
            List<CourseVm> list = new();
            switch (request.Type)
            {
                case CourseTypes.New:
                    list = new List<CourseVm>()
                        {
                            new CourseVm()
                            {
                                Name = "Course 1",
                                Description = "Course Description",
                                Id = "1",
                                Purchased = false
                            },
                            new CourseVm()
                            {
                                Name = "Course 2",
                                Description = "Course Description",
                                Id = "2",
                                Purchased = false
                            }
                        };
                    break;
                case CourseTypes.Wished:
                    list = new List<CourseVm>()
                        {
                            new CourseVm()
                            {
                                Name = "Course 3",
                                Description = "Course Description",
                                Id = "3",
                                Purchased = false
                            },
                            new CourseVm()
                            {
                                Name = "Course 4",
                                Description = "Course Description",
                                Id = "4",
                                Purchased = false
                            }
                        };
                    break;
                case CourseTypes.Current:
                    list = new List<CourseVm>()
                        {
                            new CourseVm()
                            {
                                Name = "Course 5",
                                Description = "Course Description",
                                Id = "5",
                                Purchased = true
                            },
                            new CourseVm()
                            {
                                Name = "Course 6",
                                Description = "Course Description",
                                Id = "6",
                                Purchased = true
                            }
                        };
                    break;
                case CourseTypes.Completed:
                    list = new List<CourseVm>()
                        {
                            new CourseVm()
                            {
                                Name = "Course 7",
                                Description = "Course Description",
                                Id = "7",
                                Purchased = true
                            },
                            new CourseVm()
                            {
                                Name = "Course 8",
                                Description = "Course Description",
                                Id = "8",
                                Purchased = true
                            }
                        };
                    break;
            }
            DefaultResponseObject<CoursesVm> result = new() 
            { 
                Errors = null, 
                IsSuccess = true, 
                ValidationErrors = null, 
                Value = new CoursesVm() 
                { 
                    Courses = list 
                } 
            };
            return Ok(result);
        }
    }
}
