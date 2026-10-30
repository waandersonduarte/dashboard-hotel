namespace HotelDashboard.Communication.Responses;

public class PaymentResponse
{
    public Guid Id { get; set; }
    public Guid? IdReservation { get; set; }
    public Guid? IdAccount { get; set; }
    public decimal Amount { get; set; }
    public string MethodPayment { get; set; } = string.Empty;
    public DateTime PaymentDate { get; set; }
    public string Status { get; set; } = string.Empty;
}
