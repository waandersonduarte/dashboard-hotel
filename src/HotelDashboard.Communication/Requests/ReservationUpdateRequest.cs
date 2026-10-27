namespace HotelDashboard.Communication.Requests;

public class ReservationUpdateRequest
{
    public Guid IdRoom { get; set; }
    public DateTime CheckinDate { get; set; }
    public DateTime CheckoutDate { get; set; }
}
