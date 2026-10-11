using HotelDashboard.Api.Contracts.Requests;

namespace HotelDashboard.Application.UseCases.Guest;

public interface IRegisterGuestUseCase
{
    Task<GuestResponse> ExecuteAsync(GuestRequest request);
}
