namespace HotelDashboard.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public long CodeAccount { get; set; }
    public double Value { get; set; }
    public string MethodPayment { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    public Account? Account { get; set; }
}