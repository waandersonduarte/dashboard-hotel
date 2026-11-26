using HotelDashboard.Domain.Entities;

namespace HotelDashboard.Domain.Repositories;

public interface ICheckoutRepository
{
    Task AddAsync(Checkout checkout);
    Task<bool> ExistsCheckoutAsync(Guid? idCheckin);
    Task<Checkout?> GetByIdCheckinAsync(Guid? idCheckin);
}
