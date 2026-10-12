using FluentValidation;
using HotelDashboard.Communication.Requests;

namespace HotelDashboard.Application.Validators.Guest;

public class RegisterGuestValidator : AbstractValidator<GuestRequest>
{
    public RegisterGuestValidator()
    {
        RuleFor(guest => guest.CPF).NotEmpty().WithMessage("CPF é obrigatório.");
        RuleFor(guest => guest.Telephone).NotEmpty().WithMessage("O telefone é obrigatório.");
        RuleFor(guest => guest.Name).NotEmpty().WithMessage("O nome é obrigatório.").MaximumLength(100).WithMessage("O nome não deve exceder 100 caracteres.");
        RuleFor(guest => guest.Email).NotEmpty().WithMessage("O e-mail é obrigatório.").EmailAddress().WithMessage("Formato de e-mail inválido.");
    }
}
