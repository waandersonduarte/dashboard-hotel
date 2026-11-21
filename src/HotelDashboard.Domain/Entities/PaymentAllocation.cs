namespace HotelDashboard.Domain.Entities;

public class PaymentAllocation
{
    public Guid Id { get; set; }
    public Guid IdPayment { get; set; }
    public Guid IdCharge { get; set; }
    public decimal Amount { get; set; }

    public Payment Payment { get; set; } = null!;
    public Charge Charge { get; set; } = null!;
}
