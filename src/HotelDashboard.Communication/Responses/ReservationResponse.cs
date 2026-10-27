namespace HotelDashboard.Communication.Responses;

public class ReservationResponse
{
    public Guid Id { get; set; }
    public Guid IdGuest { get; set; }
    public Guid IdRoom { get; set; }
    public DateTime CheckinDate { get; set; }
    public DateTime CheckoutDate { get; set; }
    public string Status { get; set; } = string.Empty;
}
