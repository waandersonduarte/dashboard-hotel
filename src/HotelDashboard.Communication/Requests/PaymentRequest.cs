namespace HotelDashboard.Communication.Requests;

public class PaymentRequest
{
    public int MethodPayment { get; set; }
    public decimal Amount { get; set; }
    public ICollection<PaymentAllocationRequest> Allocations { get; set; } = new List<PaymentAllocationRequest>();
}

public class PaymentAllocationRequest
{
    public Guid ChargeId { get; set; }
    public decimal Amount { get; set; }
}