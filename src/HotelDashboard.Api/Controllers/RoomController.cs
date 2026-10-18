using HotelDashboard.Application.UseCases.Room;
using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HotelDashboard.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class RoomController : ControllerBase
{
    private readonly IRoomUseCase _roomUseCase;

    public RoomController(IRoomUseCase roomUseCase)
    {
        _roomUseCase = roomUseCase;
    }

    [HttpPost]
    [ProducesResponseType(typeof(RoomResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SalvarAsync([FromBody] RoomRequest request)
    {
        var response = await _roomUseCase.SalvarAsync(request);
        return Ok(response);
    }

    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(typeof(RoomStatusResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromBody] RoomStatusRequest request)
    {
        var response = await _roomUseCase.UpdateStatusRoomAsync(id, request);
        return Ok(response);
    }

    [HttpGet("{number}")]
    [ProducesResponseType(typeof(GuestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByNumberRoom(string number)
    {
        var response = await _roomUseCase.GetByNumberRoom(number);
        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GuestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var response = await _roomUseCase.GetAll();
        return Ok(response);
    }
}