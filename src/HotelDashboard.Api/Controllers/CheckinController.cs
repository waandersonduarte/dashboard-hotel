using HotelDashboard.Application.UseCases.Checkin;
using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HotelDashboard.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class CheckinController : ControllerBase
{
    private readonly ICheckinUseCase _checkinUseCase;

    public CheckinController(ICheckinUseCase checkinUseCase)
    {
        _checkinUseCase = checkinUseCase;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CheckinResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register([FromBody] CheckinRequest request)
    {
        var result = await _checkinUseCase.SaveAsync(request);
        return Created(string.Empty, result);
    }

    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(typeof(CheckinStatusResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromBody] CheckinStatusRequest request)
    {
        var response = await _checkinUseCase.UpdateStatusAsync(id, request);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CheckinResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _checkinUseCase.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("guest/{idGuest:guid}")]
    [ProducesResponseType(typeof(List<CheckinResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdGuest(Guid idGuest)
    {
        var result = await _checkinUseCase.GetByIdGuestAsync(idGuest);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<CheckinResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _checkinUseCase.GetAllAsync();
        return Ok(result);
    }
}