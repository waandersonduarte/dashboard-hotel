using HotelDashboard.Domain.Entities;

namespace HotelDashboard.Domain.Repositories;

public interface IChargeRepository
{
    Task AddAsync(Charge charge);
    Task<Charge?> GetWithPaymentsByIdAsync(Guid id);
    Task<Charge?> GetAccommodationByReservationIdAsync(Guid reservationId);
    Task<List<Charge>> GetByReservationIdWithAllocationsAsync(Guid reservationId);
}
