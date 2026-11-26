namespace HotelDashboard.Domain.Entities;

public class Checkout
{
    public Guid Id { get; set; }
    public Guid IdCheckin { get; set; }
    public DateTime CheckoutDate { get; set; }

    public Checkin Checkin { get; set; } = null!;
}
