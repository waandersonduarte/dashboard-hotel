using FluentValidation;
using HotelDashboard.Communication.Requests;

namespace HotelDashboard.Application.Validators.Room;

public class RegisterRoomValidator : AbstractValidator<RoomRequest>
{
    public RegisterRoomValidator()
    {
        RuleFor(guest => guest.Number).NotEmpty().WithMessage("O número é obrigatório.");
        RuleFor(guest => guest.Type).NotEmpty().WithMessage("O tipo é obrigatório.").MaximumLength(50).WithMessage("O nome não deve exceder 100 caracteres.");
        RuleFor(guest => guest.Capacity.ToString()).NotEmpty().WithMessage("A capacidade é obrigatória.");
        RuleFor(guest => guest.PricePerNight.ToString()).NotEmpty().WithMessage("O preço por noite é obrigatório.");
    }
}
