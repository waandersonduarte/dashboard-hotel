using HotelDashboard.Application.UseCases.Guest;
using Microsoft.Extensions.DependencyInjection;

namespace HotelDashboard.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IGuestUseCase, GuestUseCase>();
    }
}