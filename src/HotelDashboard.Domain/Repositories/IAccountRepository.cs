using HotelDashboard.Domain.Entities;

namespace HotelDashboard.Domain.Repositories;

public interface IAccountRepository
{
    Task AddAsync(Account account);
    Task<Account?> GetByIdAsync(Guid? id);
}
