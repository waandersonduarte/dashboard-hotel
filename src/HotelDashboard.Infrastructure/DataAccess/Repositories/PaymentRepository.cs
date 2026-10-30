
using HotelDashboard.Domain.Entities;
using HotelDashboard.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelDashboard.Infrastructure.DataAccess.Repositories;

internal sealed class PaymentRepository : IPaymentRepository
{
    private readonly HotelDashboardDbContext _dbContext;

    public PaymentRepository(HotelDashboardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Payment payment) => await _dbContext.Payments.AddAsync(payment);
    public async Task<Payment?> GetByIdAsync(Guid id) => await _dbContext.Payments.FirstOrDefaultAsync(c => c.Id == id);
}
