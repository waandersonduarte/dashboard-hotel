using HotelDashboard.Domain.Enums;

namespace HotelDashboard.Domain.Entities;

public class Charge
{
    public Guid Id { get; set; }
    public Guid? IdReservation { get; set; }
    public Guid? IdAccount { get; set; }
    public Guid? IdConsumption { get; set; }
    public ChargeTypeEnum Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitAmount { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }

    public Reservation Reservation { get; set; } = null!;
    public Account Account { get; set; } = null!;
    public Consumption Consumption { get; set; } = null!;
    public ICollection<PaymentAllocation> Allocations { get; set; } = new List<PaymentAllocation>();
}
