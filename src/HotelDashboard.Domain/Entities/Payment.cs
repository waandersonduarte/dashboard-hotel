using HotelDashboard.Domain.Enums;

namespace HotelDashboard.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public Guid? IdReservation { get; set; }
    public Guid? IdAccount { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethodEnum MethodPayment { get; set; }
    public DateTime PaymentDate { get; set; }
    public StatusPaymentEnum Status { get; set; }

    public Reservation Reservation { get; set; } = null!;
    public Account Account { get; set; } = null!;
}