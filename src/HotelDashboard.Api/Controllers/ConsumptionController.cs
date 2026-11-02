using HotelDashboard.Application.UseCases.Consumption;
using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HotelDashboard.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class ConsumptionController : ControllerBase
{
    private readonly IConsumptionUseCase _consumptionUseCase;

    public ConsumptionController(IConsumptionUseCase consumptionUseCase)
    {
        _consumptionUseCase = consumptionUseCase;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ConsumptionResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register([FromBody] ConsumptionRequest request)
    {
        var result = await _consumptionUseCase.SaveAsync(request);
        return Created(string.Empty, result);
    }

    [HttpGet("{idAccount}")]
    [ProducesResponseType(typeof(List<ConsumptionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAccountAsync(Guid idAccount)
    {
        var result = await _consumptionUseCase.GetByIdAccountAsync(idAccount);
        return Ok(result);
    }
}
