

using HotelDashboard.Domain.Repositories;

namespace HotelDashboard.Infrastructure.DataAccess;

internal class UnitOfWork : IUnitOfWork
{
    private readonly HotelDashboardDbContext _dbContext;

    public UnitOfWork(HotelDashboardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CommitAsync() => await _dbContext.SaveChangesAsync();
}
