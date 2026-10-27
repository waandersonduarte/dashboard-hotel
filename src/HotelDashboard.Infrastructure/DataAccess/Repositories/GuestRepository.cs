
using HotelDashboard.Domain.Entities;
using HotelDashboard.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelDashboard.Infrastructure.DataAccess.Repositories;

internal sealed class GuestRepository : IGuestRepository
{
    private readonly HotelDashboardDbContext _dbContext;

    public GuestRepository(HotelDashboardDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Guest guest) => await _dbContext.Guests.AddAsync(guest);
    public async Task<bool> ExistByCPF(string cpf) => await _dbContext.Guests.AnyAsync(guest => guest.CPF == cpf);
    public async Task<Guest?> GetByCpf(string cpf) => await _dbContext.Guests.FirstOrDefaultAsync(guest => guest.CPF == cpf && guest.Active);
    public async Task<bool> ExistByEmail(string email) => await _dbContext.Guests.AnyAsync(guest => guest.Email == email && guest.Active);
    public async Task<List<Guest>> GetAll() => await _dbContext.Guests.Where(guest => guest.Active).ToListAsync();
    public async Task<bool> ExistById(Guid? id) => await _dbContext.Guests.AnyAsync(guest => guest.Id == id);
}