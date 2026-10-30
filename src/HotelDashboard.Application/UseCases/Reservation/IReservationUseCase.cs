using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;

namespace HotelDashboard.Application.UseCases.Reservation;

public interface IReservationUseCase
{
    Task<ReservationResponse> SaveAsync(ReservationRequest request);
    Task<ReservationResponse> UpdateAsync(Guid id, ReservationUpdateRequest request);
    Task<ReservationStatusResponse> UpdaStatusAsync(Guid id, ReservationStatusRequest request);
    Task<ReservationResponse> GetByIdAsync(Guid id);
    Task<List<ReservationResponse>> GetByIdGuestAsync(Guid idGuest);
    Task<List<ReservationResponse>> GetAllAsync();
}
