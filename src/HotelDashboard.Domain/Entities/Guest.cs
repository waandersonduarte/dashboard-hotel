namespace HotelDashboard.Domain.Entities;

public class Guest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateRegister { get; set; }
}
