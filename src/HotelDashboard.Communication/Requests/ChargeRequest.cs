namespace HotelDashboard.Communication.Requests;

public class ChargeRequest
{
    public Guid? ReservationId { get; set; }
    public Guid? AccountId { get; set; }
    public Guid? ConsumptionId { get; set; }
    public int Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitAmount { get; set; }
}
