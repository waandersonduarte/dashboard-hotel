
using HotelDashboard.Domain.Entities;
using HotelDashboard.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelDashboard.Infrastructure.DataAccess.Repositories;

internal sealed class AccountRepository : IAccountRepository
{
    private readonly HotelDashboardDbContext _dbContext;

    public AccountRepository(HotelDashboardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Account account) => await _dbContext.Accounts.AddAsync(account);
    public async Task<Account?> GetByIdAsync(Guid? id) => await _dbContext.Accounts.FirstOrDefaultAsync(c => c.Id == id);
}
