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
                                Title = "Course 1",
                                CourseDescription = "Course Description",
                                CourseId = "1",
                                Purchased = false
                            },
                            new CourseVm()
                            {
                                Title = "Course 2",
                                CourseDescription = "Course Description",
                                CourseId = "2",
                                Purchased = false
                            }
                        };
                    break;
                case CourseTypes.Wished:
                    list = new List<CourseVm>()
                        {
                            new CourseVm()
                            {
                                Title = "Course 3",
                                CourseDescription = "Course Description",
                                CourseId = "3",
                                Purchased = false
                            },
                            new CourseVm()
                            {
                                Title = "Course 4",
                                CourseDescription = "Course Description",
                                CourseId = "4",
                                Purchased = false
                            }
                        };
                    break;
                case CourseTypes.Current:
                    list = new List<CourseVm>()
                        {
                            new CourseVm()
                            {
                                Title = "Course 5",
                                CourseDescription = "Course Description",
                                CourseId = "5",
                                Purchased = true
                            },
                            new CourseVm()
                            {
                                Title = "Course 6",
                                CourseDescription = "Course Description",
                                CourseId = "6",
                                Purchased = true
                            }
                        };
                    break;
                case CourseTypes.Completed:
                    list = new List<CourseVm>()
                        {
                            new CourseVm()
                            {
                                Title = "Course 7",
                                CourseDescription = "Course Description",
                                CourseId = "7",
                                Purchased = true
                            },
                            new CourseVm()
                            {
                                Title = "Course 8",
                                CourseDescription = "Course Description",
                                CourseId = "8",
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
