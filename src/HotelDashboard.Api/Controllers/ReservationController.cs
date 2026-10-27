using HotelDashboard.Application.UseCases.Checkin;
using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HotelDashboard.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class ReservationController : ControllerBase
{
    private readonly IReservationUseCase _reservationUseCase;

    public ReservationController(IReservationUseCase reservationUseCase)
    {
        _reservationUseCase = reservationUseCase;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReservationResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register([FromBody] ReservationRequest request)
    {
        var result = await _reservationUseCase.SaveAsync(request);
        return Created(string.Empty, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ReservationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Upsate([FromRoute] Guid id, [FromBody] ReservationUpdateRequest request)
    {
        var result = await _reservationUseCase.UpdateAsync(id, request);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(typeof(ReservationStatusResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromBody] ReservationStatusRequest request)
    {
        var result = await _reservationUseCase.UpdaStatusAsync(id, request);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ReservationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _reservationUseCase.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("guest/{idGuest:guid}")]
    [ProducesResponseType(typeof(List<ReservationResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdGuest(Guid idGuest)
    {
        var result = await _reservationUseCase.GetByIdGuestAsync(idGuest);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ReservationResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _reservationUseCase.GetAllAsync();
        return Ok(result);
    }
}
