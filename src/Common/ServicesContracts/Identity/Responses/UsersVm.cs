namespace ServicesContracts.Identity.Responses;

public class UsersVm
{
    public IEnumerable<RegisteredUserVM> Users { get; set; }
    public string? Filter { get; set; }
    public PageVm PageVm { get; set; }
}