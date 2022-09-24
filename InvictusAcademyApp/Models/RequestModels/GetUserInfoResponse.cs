namespace InvictusAcademyApp.Models.RequestModels;

public class GetUserInfoResponse
{
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string? InstagramLink { get; set; }
    public string Citizenship { get; set; }
}