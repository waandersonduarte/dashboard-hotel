using HotelDashboard.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotelDashboard.Infrastructure.Extensions;

public class DatabaseMigration
{
    public static void ExecuteMigrations(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<HotelDashboardDbContext>();
        context.Database.Migrate();
    }
}
