namespace ServicesContracts.Courses.Responses;

public class PurchasedModuleInfoVm
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public bool IsCompleted { get; set; }
}
