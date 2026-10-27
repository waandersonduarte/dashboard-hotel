using HotelDashboard.Domain.Enums;

namespace HotelDashboard.Domain.Entities;

public class Reservation
{
    public Guid Id { get; set; }
    public Guid IdGuest { get; set; }
    public Guid IdRoom { get; set; }
    public DateTime CheckinDate { get; set; }
    public DateTime CheckoutDate { get; set; }
    public StatusReservationEnum Status { get; set; }

    public Guest Guest { get; set; } = new();
    public Room Room { get; set; } = new();
}
