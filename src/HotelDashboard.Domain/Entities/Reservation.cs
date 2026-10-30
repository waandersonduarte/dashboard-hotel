using HotelDashboard.Domain.Enums;

namespace HotelDashboard.Domain.Entities;

public class Reservation
{
    public Guid Id { get; set; }
    public Guid IdGuest { get; set; }
    public Guid IdRoom { get; set; }
    public DateTime CheckinDate { get; set; }
    public DateTime CheckoutDate { get; set; }
    public decimal TotalAmount { get; set; }
    public StatusReservationEnum Status { get; set; }

    public Guest Guest { get; set; } = null!;
    public Room Room { get; set; } = null!;
}
