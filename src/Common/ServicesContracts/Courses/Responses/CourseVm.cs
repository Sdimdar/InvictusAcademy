namespace ServicesContracts.Courses.Responses;

public class CourseVm
{
    public string CourseId { get; set; }
    public string Title { get; set; }
    public string CourseDescription { get; set; }
    public bool Purchased { get; set; }
}
