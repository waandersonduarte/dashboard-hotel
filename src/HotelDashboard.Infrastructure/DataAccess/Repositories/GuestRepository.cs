
using HotelDashboard.Domain.Entities;
using HotelDashboard.Domain.Repositories;

namespace HotelDashboard.Infrastructure.DataAccess.Repositories;

internal sealed class GuestRepository : IGuestRepository
{
    private readonly HotelDashboardDbContext _dbContext;

    public GuestRepository(HotelDashboardDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Guest guest) => await _dbContext.AddAsync(guest);
}
