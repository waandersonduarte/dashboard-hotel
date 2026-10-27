using HotelDashboard.Domain.Entities;
using HotelDashboard.Domain.Enums;
using HotelDashboard.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelDashboard.Infrastructure.DataAccess.Repositories;

internal class ReservationRepository : IReservationRepository
{
    private readonly HotelDashboardDbContext _dbContext;

    public ReservationRepository(HotelDashboardDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Reservation reservation) => await _dbContext.Reservations.AddAsync(reservation);

    public async Task<bool> ExistisConfirmedOrPendingAsync(Guid idGuest) => await _dbContext.Reservations.AnyAsync(r => r.IdGuest == idGuest && (r.Status == StatusReservationEnum.Confirmed || r.Status == StatusReservationEnum.Pending));
    public async Task<Reservation?> GetByIdAsync(Guid? id) => await _dbContext.Reservations
                                                                    .Include(r => r.Room)
                                                                    .Include(r => r.Guest)
                                                                    .FirstOrDefaultAsync(r => r.Id == id);
    public async Task<List<Reservation>> GetListByIdGuestAsync(Guid idGuest) => await _dbContext.Reservations.Where(r => r.IdGuest == idGuest).ToListAsync();
    public async Task<List<Reservation>> GetAllAsync() => await _dbContext.Reservations.ToListAsync();
}
