using HotelDashboard.Domain.Entities;

namespace HotelDashboard.Domain.Repositories;

public interface IGuestRepository
{
    Task AddAsync(Guest guest);
}
