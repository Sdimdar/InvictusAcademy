namespace AdminGateway.MVC.ViewModels;

public class CreateCourseAndRoomCommand
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? VideoLink { get; set; }
    public decimal Cost { get; set; }
    public bool IsActive { get; set; }
}