
using HotelDashboard.Application.Validators.Guest;
using HotelDashboard.Domain.Repositories;
using HotelDashboard.Communication.Responses;
using HotelDashboard.Communication.Requests;
using HotelDashboard.Domain.Exceptions;
using FluentValidation.Results;
using Mapster;

namespace HotelDashboard.Application.UseCases.Guest;

public sealed class GuestUseCase : IGuestUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGuestRepository _guestRepository;

    public GuestUseCase(IUnitOfWork unitOfWork, IGuestRepository guestRepository)
    {
        _unitOfWork = unitOfWork;
        _guestRepository = guestRepository;
    }

    public async Task<GuestResponse> SalvarAsync(GuestRequest request)
    {
        var validator = new RegisterGuestValidator();
        var result = validator.Validate(request);

        var existingGuestByCpf = await _guestRepository.ExistByCPF(request.CPF);
        if (existingGuestByCpf)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, "Já existe um hóspede com o mesmo CPF."));
        }

        var existingGuestByEmail = await _guestRepository.ExistByEmail(request.Email);
        if (existingGuestByEmail)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, "Já existe um hóspede com o mesmo Email."));
        }

        if(!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }

        var guest = request.Adapt<Domain.Entities.Guest>();
        await _guestRepository.AddAsync(guest);
        await _unitOfWork.CommitAsync();

        return guest.Adapt<GuestResponse>();
    }

    public async Task<GuestResponse> GetByCpf(string cpf)
    {
        var guest = await _guestRepository.GetByCpf(cpf);
        if (guest == null)
        {
            throw new NotFoundException("Hóspede não encontrado.");
        }

        return guest.Adapt<GuestResponse>();
    }

    public Task<List<GuestResponse>> GetAll()
    {
        var guests = _guestRepository.GetAll();
        if (guests == null || guests.Result.Count == 0)
        {
            throw new NotFoundException("Nenhum hóspede encontrado.");
        }
        return guests.ContinueWith(task => task.Result.Select(g => g.Adapt<GuestResponse>()).ToList());
    }

    public async Task DeleteByCpf(string cpf)
    {
        var guest = await _guestRepository.GetByCpf(cpf);
        if (guest == null)
            throw new NotFoundException("Hóspede não encontrado.");

        guest.Active = false;
        await _unitOfWork.CommitAsync();
    }
}
