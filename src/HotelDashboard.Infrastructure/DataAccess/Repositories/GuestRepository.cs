
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
}
