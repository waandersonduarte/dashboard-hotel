using HotelDashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelDashboard.Infrastructure.DataAccess;

internal class HotelDashboardDbContext : DbContext
{
    public HotelDashboardDbContext(DbContextOptions<HotelDashboardDbContext> options) : base(options) {}

    public DbSet<Guest> Guests { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Checkin> Checkins { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HotelDashboardDbContext).Assembly);
    }
}
