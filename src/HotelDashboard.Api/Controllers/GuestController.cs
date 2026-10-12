using HotelDashboard.Application.UseCases.Guest;
using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HotelDashboard.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class GuestController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(GuestResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromBody] GuestRequest request, 
        [FromServices] IGuestUseCase useCase)
    {
        var result = await useCase.SalvarAsync(request);
        return Created(string.Empty, result);
    }
}

