namespace HotelDashboard.Communication.Responses;

public class ReservationStatusResponse
{
    public Guid Id { get; set; }
    public Guid IdGuest { get; set; }
    public Guid IdRoom { get; set; }
    public string Status { get; set; } = string.Empty;
}