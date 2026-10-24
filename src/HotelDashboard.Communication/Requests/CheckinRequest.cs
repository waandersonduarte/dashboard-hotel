namespace HotelDashboard.Communication.Requests;

public class CheckinRequest
{
    public Guid IdGuest { get; set; }
    public Guid IdRoom { get; set; } 
    public int Status { get; set; }
}
