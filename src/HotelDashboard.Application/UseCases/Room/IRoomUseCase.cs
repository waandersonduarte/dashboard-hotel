using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;

namespace HotelDashboard.Application.UseCases.Room;

public interface IRoomUseCase
{
    Task<RoomResponse> SalvarAsync(RoomRequest request);
    Task<RoomStatusResponse> UpdateStatusRoomAsync(Guid id, RoomStatusRequest request);
    Task<RoomResponse> GetByNumberRoom(string number);
    Task<List<RoomResponse>> GetAll();
}