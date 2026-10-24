using HotelDashboard.Domain.Entities;

namespace HotelDashboard.Domain.Repositories;

public interface IGuestRepository
{
    Task AddAsync(Guest guest);
    Task<bool> ExistByCPF(string cpf);
    Task<bool> ExistByEmail(string email);
    Task<Guest?> GetByCpf(string cpf);
    Task<List<Guest>> GetAll();
    Task<bool> ExistById(Guid id);
}
