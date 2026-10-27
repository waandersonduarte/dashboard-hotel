using HotelDashboard.Domain.Entities;

namespace HotelDashboard.Domain.Repositories;

public interface IReservationRepository
{
    Task AddAsync(Reservation reservation);
    Task<bool> ExistisConfirmedOrPendingAsync(Guid idGuest);
    Task<Reservation?> GetByIdAsync(Guid? id);
    Task<List<Reservation>> GetListByIdGuestAsync(Guid idGuest);
    Task<List<Reservation>> GetAllAsync();
}
