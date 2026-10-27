namespace HotelDashboard.Communication.Requests;

public class CheckinRequest
{
    public Guid? IdReservation { get; set; }
    public Guid? IdGuest { get; set; }
    public Guid? IdRoom { get; set; } 
}
