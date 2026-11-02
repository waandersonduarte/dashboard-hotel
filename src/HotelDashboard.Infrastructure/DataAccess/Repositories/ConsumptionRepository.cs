
using HotelDashboard.Domain.Entities;
using HotelDashboard.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelDashboard.Infrastructure.DataAccess.Repositories;

internal sealed class ConsumptionRepository : IConsumptionRepository
{
    private readonly HotelDashboardDbContext _dbContext;

    public ConsumptionRepository(HotelDashboardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Consumption consumption) => await _dbContext.Consumptions.AddAsync(consumption);
    public async Task<Consumption?> GetByIdAsync(Guid id) => await _dbContext.Consumptions.FirstOrDefaultAsync(c => c.Id == id);
    public async Task<List<Consumption>> GetByIdListAsync(Guid idAccount) => await _dbContext.Consumptions.Where(c => c.IdAccount == idAccount).ToListAsync();
}
