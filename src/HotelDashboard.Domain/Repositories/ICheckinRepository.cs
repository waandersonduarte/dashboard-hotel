using HotelDashboard.Domain.Entities;

namespace HotelDashboard.Domain.Repositories;

public interface ICheckinRepository
{
    Task AddAsync(Checkin checkin);
    Task<bool> ExistsCheckinActiveAsync(Guid? idGuest);
    Task<Checkin?> GetByIdAsync(Guid? id);
    Task<List<Checkin>> GetListByIdGuestAsync(Guid idGuest);
    Task<List<Checkin>> GetAllAsync();
}
