namespace HotelDashboard.Communication.Responses;

public class RoomStatusResponse
{
    public Guid Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public string? Status { get; set; }
}