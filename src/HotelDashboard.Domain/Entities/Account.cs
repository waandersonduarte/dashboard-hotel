namespace HotelDashboard.Domain.Entities;

public class Account
{
    public Guid Id { get; set; }
    public string CodeCheckin { get; set; } = string.Empty;
    public char Status { get; set; }
    public double ValueTotal { get; set; }
    public DateTime? DateClosing { get; set; }
    public string? MethodPayment { get; set; }
    public Checkin? Checkin { get; set; }
}
