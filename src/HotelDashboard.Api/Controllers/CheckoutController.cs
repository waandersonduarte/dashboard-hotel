using HotelDashboard.Application.UseCases.Checkout;
using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HotelDashboard.Api.Controllers;
[Route("[controller]")]
[ApiController]
public class CheckoutController : ControllerBase
{
    private readonly ICheckoutUseCase _checkoutUseCase;

    public CheckoutController(ICheckoutUseCase checkoutUseCase)
    {
        _checkoutUseCase = checkoutUseCase;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CheckoutResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register([FromBody] CheckoutRequest request)
    {
        var result = await _checkoutUseCase.ExecuteAsync(request);
        return Created(string.Empty, result);
    }
}