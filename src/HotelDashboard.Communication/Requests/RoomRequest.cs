namespace HotelDashboard.Communication.Requests;

public class RoomRequest
{
    public string Number { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public long Capacity { get; set; }
    public double PricePerNight { get; set; }
}
