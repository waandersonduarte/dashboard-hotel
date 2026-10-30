using HotelDashboard.Domain.Entities;

namespace HotelDashboard.Domain.Repositories;

public interface IPaymentRepository
{
    Task AddAsync(Payment payment);
    Task<Payment?> GetByIdAsync(Guid id);
}
