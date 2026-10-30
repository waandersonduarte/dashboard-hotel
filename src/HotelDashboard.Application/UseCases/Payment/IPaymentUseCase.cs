using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;

namespace HotelDashboard.Application.UseCases.Payment;

public interface IPaymentUseCase
{
    Task<PaymentResponse> SaveAsync(PaymentRequest request);
}
