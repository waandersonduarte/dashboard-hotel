using HotelDashboard.Domain.Enums;

namespace HotelDashboard.Domain.Entities;

public class Account
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid IdCheckin { get; set; }
    public DateTime? DateClosing { get; set; }
    public StatusAccountEnum Status { get; set; }

    public Checkin Checkin { get; set; } = null!;
    public ICollection<Consumption> Consumptions { get; set; } = new List<Consumption>();
    public ICollection<Charge> Charges { get; set; } = new List<Charge>();
}
