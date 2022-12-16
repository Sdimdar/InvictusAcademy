namespace ServicesContracts.Jitsi.Models;

public class AllStreamingRoomsVm
{
    public List<StreamingRoomVm> StreamingRooms { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? Filter { get; set; }
}