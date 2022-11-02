namespace ServicesContracts.Courses.Responses;

public class CourseInfoVm
{
    public CourseVm CourseData { get; set; }
    public List<int> ModulesId { get; set; }
}