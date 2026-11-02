using HotelDashboard.Domain.Entities;

namespace HotelDashboard.Domain.Repositories;

public interface IConsumptionRepository
{
    Task AddAsync(Consumption consumption);
    Task<Consumption?> GetByIdAsync(Guid id);
    Task<List<Consumption>> GetByIdListAsync(Guid idAccount);
}
