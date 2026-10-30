using HotelDashboard.Application.UseCases.Payment;
using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HotelDashboard.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentUseCase _paymentUseCase;

    public PaymentController(IPaymentUseCase paymentUseCase)
    {
        _paymentUseCase = paymentUseCase;
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register([FromBody] PaymentRequest request)
    {
        var result = await _paymentUseCase.SaveAsync(request);
        return Created(string.Empty, result);
    }
}