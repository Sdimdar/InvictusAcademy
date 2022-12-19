using ServicesContracts.Courses.Requests.Courses.Commands;

namespace ServicesContracts.Courses.Responses;

public class CourseVm
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SecondName { get; set; }
    public string SecondDescription { get; set; }
    public List<CoursePointsVm>? CoursePoints { get; set; }
    public decimal Cost { get; set; }
    public string? VideoLink { get; set; }
    public string? PreviewLink { get; set; }
    public bool Purchased { get; set; }
}
