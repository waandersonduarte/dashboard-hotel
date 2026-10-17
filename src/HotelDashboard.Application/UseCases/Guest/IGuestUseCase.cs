using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;

namespace HotelDashboard.Application.UseCases.Guest;

public interface IGuestUseCase
{
    Task<GuestResponse> SalvarAsync(GuestRequest request);
    Task<GuestResponse> GetByCpf(string cpf);
    Task<List<GuestResponse>> GetAll();
    Task DeleteByCpf(string cpf);
}
