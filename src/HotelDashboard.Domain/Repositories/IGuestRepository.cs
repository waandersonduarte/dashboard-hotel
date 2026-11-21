using HotelDashboard.Domain.Entities;

namespace HotelDashboard.Domain.Repositories;

public interface IGuestRepository
{
    Task AddAsync(Guest guest);
    Task<bool> ExistByCPF(string cpf);
    Task<bool> ExistByEmail(string email);
    Task<Guest?> GetByCpf(string cpf);
    Task<Guest?> GetByIdAsync(Guid id);
    Task<List<Guest>> GetAll();
    Task<bool> ExistById(Guid? id);
}
