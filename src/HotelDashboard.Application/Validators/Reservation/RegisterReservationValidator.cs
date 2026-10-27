using FluentValidation;
using HotelDashboard.Communication.Requests;

namespace HotelDashboard.Application.Validators.Checkin;

public class RegisterReservationValidator : AbstractValidator<ReservationRequest>
{
    public RegisterReservationValidator()
    {
        RuleFor(reservation => reservation.IdGuest).NotEmpty().WithMessage("O id do hospede é obrigatório.");
        RuleFor(reservation => reservation.IdRoom).NotEmpty().WithMessage("O id do quarto é obrigatório.");
        RuleFor(reservation => reservation.CheckinDate).NotEmpty().WithMessage("A data de checkin é obrigatória.");
        RuleFor(reservation => reservation.CheckoutDate).NotEmpty().WithMessage("A data de checkout é obrigatória.");
    }
}
