using Microsoft.EntityFrameworkCore;

namespace HotelDashboard.Infrastructure.DataAccess;

internal class HotelDashboardDbContext : DbContext
{
    public HotelDashboardDbContext(DbContextOptions<HotelDashboardDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HotelDashboardDbContext).Assembly);
    }
}
