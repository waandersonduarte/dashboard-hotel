using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;

namespace HotelDashboard.Application.UseCases.Guest;

public interface IGuestUseCase
{
    Task<GuestResponse> SalvarAsync(GuestRequest request);
}
