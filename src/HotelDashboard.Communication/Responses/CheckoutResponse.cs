namespace HotelDashboard.Communication.Responses;

public class CheckoutResponse
{
    public Guid Id { get; set; }
    public Guid IdCheckin { get; set; }
    public DateTime DateCheckout { get; set; }

    public string CheckInStatus { get; set; } = string.Empty;
    public string AccountStatus { get; set; } = string.Empty;
    public string RoomStatus { get; set; } = string.Empty;
}