namespace HotelDashboard.Communication.Requests;

public class GetPendingChargesRequest
{
    public Guid? IdAccount { get; set; }
    public Guid? IdReservation { get; set; }
}
