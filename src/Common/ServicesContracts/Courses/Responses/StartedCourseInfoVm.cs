namespace ServicesContracts.Courses.Responses;

public class StartedCourseInfoVm
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
}