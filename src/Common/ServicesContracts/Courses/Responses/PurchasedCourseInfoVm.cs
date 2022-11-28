namespace ServicesContracts.Courses.Responses;

public class PurchasedCourseInfoVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ShortModuleInfoVm> Modules { get; set; }
}