namespace HotelDashboard.Communication.Responses;

public class CheckinResponse
{
    public Guid Id { get; set; }
    public Guid IdGuest { get; set; }
    public Guid IdRoom { get; set; }
    public DateTime? EntryDate { get; set; }
    public DateTime? DepartureDate { get; set; }
    public string Status { get; set; } = string.Empty;
}
