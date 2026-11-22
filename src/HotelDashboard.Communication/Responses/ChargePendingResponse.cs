namespace HotelDashboard.Communication.Responses;

public class ChargePendingResponse
{
    public decimal TotalPending { get; set; }
    public List<ChargeResponse> Charges { get; set; } = new List<ChargeResponse>();
}

public class ChargeResponse
{
    public Guid IdCharge { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal RemainingAmount { get; set; }
}
