namespace HotelDashboard.Communication.Responses;
public class ConsumptionResponse
{
    public Guid Id { get; set; }
    public Guid IdAccount { get; set; }
    public string Item { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public int Quantity { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}