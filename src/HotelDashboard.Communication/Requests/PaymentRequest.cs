namespace HotelDashboard.Communication.Requests;

public class PaymentRequest
{
    public Guid? IdReservation { get; set; }
    public Guid? IdAccount { get; set; }
    public int MethodPayment { get; set; }
}