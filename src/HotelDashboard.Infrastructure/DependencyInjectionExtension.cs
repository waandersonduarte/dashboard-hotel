using HotelDashboard.Domain.Repositories;
using HotelDashboard.Infrastructure.DataAccess;
using HotelDashboard.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelDashboard.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGuestRepository, GuestRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<ICheckinRepository, CheckinRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();

        services.AddDbContext<HotelDashboardDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
        });
    }
}