namespace HotelDashboard.Domain.Entities;

public class Checkin
{
    public Guid Id { get; set; }
    public string GuestCode { get; set; } = string.Empty;
    public string RoomCode { get; set; } = string.Empty;
    public DateTime? EntryDate { get; set; }
    public DateTime? DepartureDate { get; set; }
    public char? Status { get; set; }

    public Guest? Guest { get; set; }
    public Room? Room { get; set; }
    //public List<Consumo> Consumos { get; set; }
}
