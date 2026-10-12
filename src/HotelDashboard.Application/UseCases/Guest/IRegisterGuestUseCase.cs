using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;

namespace HotelDashboard.Application.UseCases.Guest;

public interface IRegisterGuestUseCase
{
    Task<GuestResponse> ExecuteAsync(GuestRequest request);
}
