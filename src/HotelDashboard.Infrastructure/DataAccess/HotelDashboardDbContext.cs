using Microsoft.EntityFrameworkCore;

namespace HotelDashboard.Infrastructure.DataAccess;

internal class HotelDashboardDbContext : DbContext
{
    public HotelDashboardDbContext(DbContextOptions<HotelDashboardDbContext> options) : base(options) {}
}
