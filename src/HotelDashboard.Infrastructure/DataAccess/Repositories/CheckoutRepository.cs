
using HotelDashboard.Domain.Entities;
using HotelDashboard.Domain.Enums;
using HotelDashboard.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelDashboard.Infrastructure.DataAccess.Repositories;

internal sealed class CheckoutRepository : ICheckoutRepository
{
    private readonly HotelDashboardDbContext _dbContext;

    public CheckoutRepository(HotelDashboardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Checkout checkout) => await _dbContext.Checkouts.AddAsync(checkout);
    public async Task<bool> ExistsCheckoutAsync(Guid? idCheckin) => await _dbContext.Checkouts.AnyAsync(c => c.IdCheckin == idCheckin && c.Checkin.Status == StatusCheckinEnum.Active);
    public Task<Checkout?> GetByIdCheckinAsync(Guid? idCheckin)
    {
        throw new NotImplementedException();
    }
}
