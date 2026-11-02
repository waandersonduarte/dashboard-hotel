using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;

namespace HotelDashboard.Application.UseCases.Consumption;

public interface IConsumptionUseCase
{
    Task<ConsumptionResponse> SaveAsync(ConsumptionRequest request);
    Task<List<ConsumptionResponse>> GetByIdAccountAsync(Guid idAccount);
}
