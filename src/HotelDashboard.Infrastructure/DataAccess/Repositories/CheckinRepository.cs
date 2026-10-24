
using HotelDashboard.Domain.Entities;
using HotelDashboard.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelDashboard.Infrastructure.DataAccess.Repositories;

internal sealed class CheckinRepository : ICheckinRepository
{
    private readonly HotelDashboardDbContext _dbContext;

    public CheckinRepository(HotelDashboardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Checkin checkin) => await _dbContext.Checkins.AddAsync(checkin);
    public async Task<bool> ExistsAsync(Guid idGuest) => await _dbContext.Checkins.AnyAsync(c => c.IdGuest == idGuest);
    public async Task<List<Checkin>> GetListByIdGuestAsync(Guid idGuest) => await _dbContext.Checkins.Where(c => c.IdGuest == idGuest).ToListAsync();
    public async Task<Checkin?> GetByIdAsync(Guid id) => await _dbContext.Checkins.FirstOrDefaultAsync(c => c.Id == id);
    public async Task<List<Checkin>> GetAllAsync() => await _dbContext.Checkins.ToListAsync();
}
