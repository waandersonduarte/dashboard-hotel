using HotelDashboard.Application.UseCases.Guest;
using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HotelDashboard.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class GuestController : ControllerBase
{
    private readonly IGuestUseCase _guestUseCase;

    public GuestController(IGuestUseCase guestUseCase)
    {
        _guestUseCase = guestUseCase;
    }

    [HttpPost]
    [ProducesResponseType(typeof(GuestResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] GuestRequest request)
    {
        var result = await _guestUseCase.SalvarAsync(request);
        return Created(string.Empty, result);
    }

    [HttpGet("{cpf}")]
    [ProducesResponseType(typeof(GuestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCpf(string cpf)
    {
        var result = await _guestUseCase.GetByCpf(cpf);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GuestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _guestUseCase.GetAll();
        return Ok(result);
    }

    [HttpDelete("{cpf}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByCpf(string cpf)
    {
        await _guestUseCase.DeleteByCpf(cpf);
        return NoContent();
    }
}

