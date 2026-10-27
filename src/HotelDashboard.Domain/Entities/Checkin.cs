using HotelDashboard.Domain.Enums;

namespace HotelDashboard.Domain.Entities;

public class Checkin
{
    public Guid Id { get; set; }
    public Guid? IdGuest { get; set; }
    public Guid? IdRoom { get; set; }
    public DateTime? EntryDate { get; set; }
    public DateTime? DepartureDate { get; set; }
    public StatusCheckinEnum Status { get; set; }

    public Guest Guest { get; set; } = new();
    public Room Room { get; set; } = new ();
    //public List<Consumo> Consumos { get; set; }
}
