namespace HotelDashboard.Communication.Responses;

public class PaymentResponse
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string MethodPayment { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime PaymentDate { get; set; }
    public ICollection<PaymentAllocationResponse> Allocations { get; set; } = new List<PaymentAllocationResponse>();
}

public class PaymentAllocationResponse
{
    public Guid Id { get; set; }
    public Guid ChargeId { get; set; }
    public decimal Amount { get; set; }
}
