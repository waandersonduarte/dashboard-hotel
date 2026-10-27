namespace HotelDashboard.Communication.Requests;

public class ReservationRequest
{
    public Guid IdGuest { get; set; }
    public Guid IdRoom { get; set; }
    public DateTime CheckinDate { get; set; }
    public DateTime CheckoutDate { get; set; }
}
