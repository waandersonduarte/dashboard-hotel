using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;

namespace HotelDashboard.Application.UseCases.Checkin;

public interface ICheckinUseCase
{
    Task<CheckinResponse> SaveAsync(CheckinRequest request);
    Task<CheckinStatusResponse> UpdateStatusAsync(Guid id, CheckinStatusRequest request);
    Task<CheckinResponse> GetByIdAsync(Guid id);
    Task<List<CheckinResponse>> GetByIdGuestAsync(Guid idGuest);
    Task<List<CheckinResponse>> GetAllAsync();
}
