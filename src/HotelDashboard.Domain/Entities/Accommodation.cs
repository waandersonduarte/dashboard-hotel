using HotelDashboard.Domain.Enums;

namespace HotelDashboard.Domain.Entities;

public class Accommodation
{
    public Guid Id { get; set; }
    public string CodeGuest { get; set; } = string.Empty;
    public string CodeRoom { get; set; } = string.Empty;
    public DateTime EntryDate { get; set; }
    public DateTime? DepartureDate { get; set; }
    public StatusCheckinEnum Status { get; set; }

    public Guest Guest { get; set; } = new();
    public Room Room { get; set; } = new ();
}