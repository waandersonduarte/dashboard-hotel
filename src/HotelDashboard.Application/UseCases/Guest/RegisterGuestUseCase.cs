
using HotelDashboard.Domain.Repositories;
using HotelDashboard.Communication.Responses;
using HotelDashboard.Communication.Requests;
using HotelDashboard.Domain.Exceptions;
using FluentValidation.Results;
using Mapster;

namespace HotelDashboard.Application.UseCases.Guest;

public class RegisterGuestUseCase : IRegisterGuestUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGuestRepository _guestRepository;

    public RegisterGuestUseCase(IUnitOfWork unitOfWork, IGuestRepository guestRepository)
    {
        _unitOfWork = unitOfWork;
        _guestRepository = guestRepository;
    }

    public async Task<GuestResponse> ExecuteAsync(GuestRequest request)
    {
        await GuestValidationAsync(request);

        var guest = request.Adapt<Domain.Entities.Guest>();
        guest.DateRegister = DateTime.Now;
        await _guestRepository.AddAsync(guest);
        await _unitOfWork.CommitAsync();

        return new GuestResponse
        {
            Id = guest.Id,
            Name = guest.Name,
            CPF = guest.CPF,
            Email = guest.Email,
            Telephone = guest.Telephone,
            DateRegister = guest.DateRegister
        };
    }

    private async Task GuestValidationAsync(GuestRequest request)
    {
        var validator = new RegisterGuestValidator();
        var result = validator.Validate(request);

        var existingGuest = await _guestRepository.ExistByCPF(request.CPF);
        if (existingGuest)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, "Já existe um hóspede com o mesmo CPF."));
        }

        if(!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
