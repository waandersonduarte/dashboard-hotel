using HotelDashboard.Domain.Enums;
using HotelDashboard.Domain.Exceptions;
using HotelDashboard.Domain.Repositories;

namespace HotelDashboard.Application.Services.Reservations;

public class ReservationConfirmationService : IReservationConfirmationService
{
    private readonly IChargeRepository _chargeRepository;
    private readonly IReservationRepository _reservationRepository;

    public ReservationConfirmationService(
        IChargeRepository chargeRepository, 
        IReservationRepository reservationRepository)
    {
        _chargeRepository = chargeRepository;
        _reservationRepository = reservationRepository;
    }

    public async Task ReservationConfirmationServiceAsync(Guid IdReservation)
    {
        var reservation = await _reservationRepository.GetByIdAsync(IdReservation);
        if (reservation == null)
            throw new NotFoundException("Reserva não encontrada.");

        if (reservation.Status != StatusReservationEnum.Pending)
            return;

        var charges = await _chargeRepository.GetByReservationIdWithAllocationsAsync(IdReservation);

        var accommodationCharges = charges
            .Where(x => x.Type == ChargeTypeEnum.Accommodation)
            .ToList();

        if (!accommodationCharges.Any())
            return;

        var totalAccommodation = accommodationCharges
            .Sum(x => x.Amount);

        var totalPaid = accommodationCharges
            .SelectMany(x => x.Allocations)
            .Where(x => x.Payment.Status == StatusPaymentEnum.Paid)
            .Sum(x => x.Amount);

        if (totalPaid >= totalAccommodation)
        {
            reservation.Status = StatusReservationEnum.Confirmed;
        }
    }
}
