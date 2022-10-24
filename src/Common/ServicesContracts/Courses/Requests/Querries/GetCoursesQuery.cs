namespace ServicesContracts.Courses.Requests.Querries;

public class GetCoursesQuery
{
    public int UserId { get; set; }
    public CourseTypes Type { get; set; }
}