using HotelDashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelDashboard.Infrastructure.DataAccess;

internal class HotelDashboardDbContext : DbContext
{
    public HotelDashboardDbContext(DbContextOptions<HotelDashboardDbContext> options) : base(options) {}

    public DbSet<Guest> Guests { get; set; }
    public DbSet<Room> Rooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HotelDashboardDbContext).Assembly);
    }
}
