namespace HotelDashboard.Communication.Responses;

public class RoomResponse
{
    public Guid Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public long Capacity { get; set; }
    public double PricePerNight { get; set; }
    public string Status { get; set; } = string.Empty;
}