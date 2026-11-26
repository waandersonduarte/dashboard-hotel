using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;

namespace HotelDashboard.Application.UseCases.Checkout;

public interface ICheckoutUseCase
{
    Task<CheckoutResponse> ExecuteAsync(CheckoutRequest request);
}
