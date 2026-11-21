
using HotelDashboard.Domain.Entities;
using HotelDashboard.Domain.Enums;
using HotelDashboard.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelDashboard.Infrastructure.DataAccess.Repositories;

internal sealed class ChargeRepository : IChargeRepository
{
    private readonly HotelDashboardDbContext _dbContext;

    public ChargeRepository(HotelDashboardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Charge charge) => await _dbContext.Charges.AddAsync(charge);
    public async Task<Charge?> GetWithPaymentsByIdAsync(Guid id)
    {
        return await _dbContext.Charges
            .Include(charge => charge.Allocations)
                .ThenInclude(charge => charge.Payment)
            .FirstOrDefaultAsync(charge => charge.Id == id);
    }
    

    public async Task<Charge?> GetAccommodationByReservationIdAsync(Guid reservationId)
    {
        return await _dbContext.Charges
            .FirstOrDefaultAsync(charge => 
                charge.IdReservation == reservationId && 
                charge.Type == ChargeTypeEnum.Accommodation);
    } 

    public async Task<List<Charge>> GetByReservationIdWithAllocationsAsync(Guid reservationId)
    {
        return await _dbContext.Charges
            .Include(charge => charge.Allocations)
                .ThenInclude(charge => charge.Payment)
            .Where(charge => charge.IdReservation == reservationId)
            .ToListAsync();
    }
}
