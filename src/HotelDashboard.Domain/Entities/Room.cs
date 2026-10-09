namespace HotelDashboard.Domain.Entities;
public class Room
{
    public Guid Id { get; set; }
    public long Number { get; set; }
    public string Type { get; set; } = string.Empty;
    public long Capacity { get; set; }
    public double PricePerNight { get; set; }
    public char Status { get; set; }
}