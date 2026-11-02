using FluentValidation;
using HotelDashboard.Communication.Requests;

namespace HotelDashboard.Application.Validators.Consumption;

public class RegisterConsumptionValidator : AbstractValidator<ConsumptionRequest>
{
    public RegisterConsumptionValidator()
    {
        RuleFor(checkin => checkin.IdAccount).NotEmpty().WithMessage("A conta é obrigatória.");
        RuleFor(checkin => checkin.Item).NotEmpty().WithMessage("O item é obrigatório.");
        RuleFor(checkin => checkin.Description).NotEmpty().WithMessage("A descrição é obrigatória.");
        RuleFor(checkin => checkin.Value).NotEmpty().WithMessage("O valor é obrigatório.");
        RuleFor(checkin => checkin.Quantity).NotEmpty().WithMessage("A quantidade é obrigatória.");
    }
}
