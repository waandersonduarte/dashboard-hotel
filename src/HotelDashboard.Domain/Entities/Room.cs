using HotelDashboard.Domain.Enums;

namespace HotelDashboard.Domain.Entities;
public class Room
{
    public Guid Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public long Capacity { get; set; }
    public double PricePerNight { get; set; }
    public StatusRoomEnum Status { get; set; }
}