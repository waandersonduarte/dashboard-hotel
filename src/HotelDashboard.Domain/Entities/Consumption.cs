using HotelDashboard.Domain.Enums;

namespace HotelDashboard.Domain.Entities;

public class Consumption
{
    public Guid Id { get; set; }
    public Guid IdAccount { get; set; }
    public string Item { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public int Quantity { get; set; }
    public DateTime Date { get; set; }
    public StatusConsumptionEnum Status { get; set; }

    public Account Account { get; set; } = null!;
}
