using System;
using HotelDashboard.Domain.Entities;

namespace HotelDashboard.Domain.Repositories;

public interface IRoomRepository
{
    Task AddAsync(Room room);
    Task<Room?> GetByNumberRoomAsync(string number);
    Task<Room?> GetByIdRoomAsync(Guid id);
    Task<bool> ExistByNumberRoomAsync(string number);
    Task<bool> ExistByIdAvailableRoomAsync(Guid id);
    Task<int> TotalNumberOfRoomsAsync();
    Task<int> TotalNumberOfOccupiedRoomsAsync();
    Task<int> TotalNumberOfAvailableRoomsAsync();
    Task<List<Room>> GetAllAsync();
}
