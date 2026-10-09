namespace HotelDashboard.Domain.Entities;

public class Checkout
{
    public Guid Id { get; set; }
    public string CodeCheckin { get; set; } = string.Empty;
    public Checkin? Checkin { get; set; }
}
