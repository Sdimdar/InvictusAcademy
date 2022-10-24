namespace ServicesContracts.Courses.Responses;

public class CourseVm
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public string? VideoLink { get; set; }
    public bool Purchased { get; set; }
}
