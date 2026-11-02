namespace HotelDashboard.Communication.Requests;

public class ConsumptionRequest
{
    public Guid IdAccount { get; set; }
    public string Item { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public int Quantity { get; set; }
}