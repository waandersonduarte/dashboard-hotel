using HotelDashboard.Communication.Requests;
using HotelDashboard.Domain.Entities;
using HotelDashboard.Domain.Enums;
using HotelDashboard.Domain.Exceptions;
using HotelDashboard.Domain.Repositories;

namespace HotelDashboard.Application.Services.Charges;

public class ChargeService : IChargeService
{
    private readonly IChargeRepository _chargeRepository;

    public ChargeService(IChargeRepository chargeRepository)
    {
        _chargeRepository = chargeRepository;
    }

    public async Task CreateAsync(ChargeRequest request)
    {
        if (request.Quantity <= 0)
            throw new ErrorOnValidationException("A quantidade deve ser maior que zero.");

        if (request.UnitAmount < 0)
            throw new ErrorOnValidationException("O valor unitário não pode ser negativo.");

        if (!request.ReservationId.HasValue && !request.AccountId.HasValue)
            throw new ErrorOnValidationException("A cobrança deve estar associada a uma reserva ou conta.");

        var charge = new Charge
        {
            IdReservation = request.ReservationId,
            IdAccount = request.AccountId,
            IdConsumption = request.ConsumptionId,
            Type = (ChargeTypeEnum)request.Type,
            Description = request.Description,
            Quantity = request.Quantity,
            UnitAmount = request.UnitAmount,
            Amount = request.Quantity * request.UnitAmount,
            Date = DateTime.UtcNow
        };
        await _chargeRepository.AddAsync(charge);
    }
}
