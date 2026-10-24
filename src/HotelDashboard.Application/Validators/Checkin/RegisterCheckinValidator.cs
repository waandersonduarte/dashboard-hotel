using FluentValidation;
using HotelDashboard.Communication.Requests;
using HotelDashboard.Domain.Enums;

namespace HotelDashboard.Application.Validators.Checkin;

public class RegisterCheckinValidator : AbstractValidator<CheckinRequest>
{
    public RegisterCheckinValidator()
    {
        RuleFor(checkin => checkin.IdGuest).NotEmpty().WithMessage("O id do hospede é obrigatório.");
        RuleFor(checkin => checkin.IdRoom).NotEmpty().WithMessage("O id do quarto é obrigatório.");
        RuleFor(checkin => (StatusCheckinEnum)checkin.Status).IsInEnum().WithMessage("O status informado é inválido.");
    }
}
