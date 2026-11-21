using HotelDashboard.Domain.Enums;

namespace HotelDashboard.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethodEnum MethodPayment { get; set; }
    public DateTime PaymentDate { get; set; }
    public StatusPaymentEnum Status { get; set; }

    public ICollection<PaymentAllocation> Allocations { get; set; } = new List<PaymentAllocation>();
}