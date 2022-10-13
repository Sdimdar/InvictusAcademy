namespace ServicesContracts.Identity.Responses;

public class UsersVm
{
    public IEnumerable<UserVm> Users { get; set; }
    public string? Filter { get; set; }
    public PageVm PageVm { get; set; }
}