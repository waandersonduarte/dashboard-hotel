namespace HotelDashboard.Domain.Entities;

public class Consumption
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Value { get; set; }
    public long Quantity { get; set; }
    public string CodeAccount { get; set; } = string.Empty;
    public char Status { get; set; }
    public DateTime Date { get; set; }
    public Account? Account { get; set; }
}
