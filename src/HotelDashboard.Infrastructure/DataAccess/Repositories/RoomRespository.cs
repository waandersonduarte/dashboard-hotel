using HotelDashboard.Domain.Entities;
using HotelDashboard.Domain.Enums;
using HotelDashboard.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelDashboard.Infrastructure.DataAccess.Repositories;
internal sealed class RoomRepository : IRoomRepository
{
    private readonly HotelDashboardDbContext _dbContext;

    public RoomRepository(HotelDashboardDbContext context)
    {
        _dbContext = context;
    }

    public async Task AddAsync(Room room) => await _dbContext.Rooms.AddAsync(room);
    public async Task<bool> ExistByNumberRoomAsync(string number) => await _dbContext.Rooms.AnyAsync(r => r.Number == number);
    public async Task<Room?> GetByIdRoomAsync(Guid id) => await _dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == id);
    public async Task<Room?> GetByNumberRoomAsync(string number) => await _dbContext.Rooms.FirstOrDefaultAsync(r => r.Number == number);
    public async Task<int> TotalNumberOfAvailableRoomsAsync() => await _dbContext.Rooms.CountAsync(r => r.Status == StatusRoomEnum.Available);
    public async Task<int> TotalNumberOfOccupiedRoomsAsync() => await _dbContext.Rooms.CountAsync(r => r.Status == StatusRoomEnum.Occupied);
    public async Task<int> TotalNumberOfRoomsAsync() => await _dbContext.Rooms.CountAsync();
    public async Task<List<Room>> GetAllAsync() => await _dbContext.Rooms.ToListAsync();
}