namespace HotelDashboard.Application.Services.Reservations;

public interface IReservationConfirmationService
{
    Task ReservationConfirmationServiceAsync(Guid IdReservation);
}
