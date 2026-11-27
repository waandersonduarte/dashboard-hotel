using FluentValidation.Results;
using HotelDashboard.Application.Services.Charges;
using HotelDashboard.Application.Validators.Checkin;
using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;
using HotelDashboard.Domain.Enums;
using HotelDashboard.Domain.Exceptions;
using HotelDashboard.Domain.Repositories;
using Mapster;

namespace HotelDashboard.Application.UseCases.Reservation;

public sealed class ReservationUseCase : IReservationUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReservationRepository _reservationRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IChargeService _chargeService;

    public ReservationUseCase(IUnitOfWork unitOfWork, IReservationRepository reservationRepository, IRoomRepository roomRepository, IGuestRepository guestRepository, IChargeService chargeService)
    {
        _unitOfWork = unitOfWork;
        _reservationRepository = reservationRepository;
        _roomRepository = roomRepository;
        _guestRepository = guestRepository;
        _chargeService = chargeService;
    }

    public async Task<ReservationResponse> SaveAsync(ReservationRequest request)
    {
        var validator = new RegisterReservationValidator();
        var result = validator.Validate(request);

        var existingReservation = await _reservationRepository.ExistisConfirmedOrPendingAsync(request.IdGuest);
        if (existingReservation)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, "O hospede já possui uma reserva pendente ou confirmada."));
        }

        if(request.CheckinDate >= request.CheckoutDate)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, "A data de check-in deve ser anterior à data de check-out."));
        }

        var existingGuest = await _guestRepository.ExistById(request.IdGuest);
        if(!existingGuest)
            throw new NotFoundException("Hóspede não encontrado.");

        var room = await _roomRepository.GetByIdRoomAsync(request.IdRoom);
        if(room == null)
            throw new NotFoundException("Quarto não encontrado.");

        var isAvailable = await _roomRepository.IsAvailableAsync(
            request.IdRoom,
            request.CheckinDate,
            request.CheckoutDate);
        if(!isAvailable)
            result.Errors.Add(new ValidationFailure(string.Empty, "O quarto informado não está disponível."));

        if(!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }

        var guest = await _guestRepository.GetByIdAsync(request.IdGuest);

        var reservation = request.Adapt<Domain.Entities.Reservation>();
        var totalDays = (reservation.CheckoutDate.Date - reservation.CheckinDate.Date).Days;

        reservation.Status = StatusReservationEnum.Pending;
        room!.Status = StatusRoomEnum.Reserved;

        await _reservationRepository.AddAsync(reservation);

        await _chargeService.CreateAsync(new ChargeRequest
        {
            ReservationId = reservation.Id,
            Type = (int)ChargeTypeEnum.Accommodation,
            Description = $"Cobrança referente à reserva do quarto {room.Number} para o hóspede {guest!.Name}.",
            Quantity = totalDays,
            UnitAmount = room.DailyRate
        });

        await _unitOfWork.CommitAsync();
        return reservation.Adapt<ReservationResponse>();
    }

    public async Task<ReservationResponse> GetByIdAsync(Guid id)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id);
        if(reservation == null)
            throw new NotFoundException("Reserva não encontrada.");
        
        return reservation.Adapt<ReservationResponse>();
    }

    public async Task<List<ReservationResponse>> GetByIdGuestAsync(Guid idGuest)
    {
        var reservationsByGuest = await _reservationRepository.GetListByIdGuestAsync(idGuest);
        if(!reservationsByGuest.Any())
            throw new NotFoundException("Nenhuma reserva encontrada para o hóspede informado.");
        
        return reservationsByGuest.Adapt<List<ReservationResponse>>();
    }

    public async Task<List<ReservationResponse>> GetAllAsync()
    {
        var Allreservations = await _reservationRepository.GetAllAsync();
        if(!Allreservations.Any())
            throw new NotFoundException("Nenhuma reserva encontrada.");
        
        return Allreservations.Adapt<List<ReservationResponse>>();
    }

    public async Task<ReservationResponse> UpdateAsync(Guid id, ReservationUpdateRequest request)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id);
        if(reservation == null)
            throw new NotFoundException("Reserva não encontrada.");

        if(reservation.Status == StatusReservationEnum.Canceled || reservation.Status == StatusReservationEnum.Completed)
            throw new ErrorOnValidationException("Reserva cancelada ou finalizada.");
        
        if (reservation.Status == StatusReservationEnum.CheckedIn)
            throw new ErrorOnValidationException("Não é possível atualizar uma reserva com check-in realizado.");
    
        
        var newIdRoom = request.IdRoom ?? reservation.IdRoom;
        var newCheckinDate = request.CheckinDate ?? reservation.CheckinDate;
        var newCheckoutDate = request.CheckoutDate ?? reservation.CheckoutDate;

        if(newCheckinDate >= newCheckoutDate)
            throw new ErrorOnValidationException("A data de check-in deve ser anterior à data de check-out.");
        
        if (newCheckoutDate <= newCheckinDate)
            throw new ErrorOnValidationException("A data de checkout deve ser posterior ao check-in.");
        
        // Verifica se houve alteração do quarto ou período
        var hasChanges =
            newIdRoom != reservation.IdRoom ||
            newCheckinDate != reservation.CheckinDate ||
            newCheckoutDate != reservation.CheckoutDate;

        if (hasChanges)
        {
            var room = await _roomRepository.GetByIdRoomAsync(newIdRoom);

            if (room == null)
                throw new NotFoundException("Quarto não encontrado.");

            // Deve verificar conflitos com outras reservas
            // e estadias, desconsiderando a reserva atual.
            var isAvailable = await _roomRepository.IsAvailableAsync(
                newIdRoom,
                newCheckinDate,
                newCheckoutDate,
                id);

            if (!isAvailable)
                throw new ErrorOnValidationException("O quarto não está disponível para o período informado.");
        }
        reservation.IdRoom = newIdRoom;
        reservation.CheckinDate = newCheckinDate;
        reservation.CheckoutDate = newCheckoutDate;

        await _unitOfWork.CommitAsync();

        return reservation.Adapt<ReservationResponse>();
    }

    public async Task<ReservationStatusResponse> UpdaStatusAsync(Guid id, ReservationStatusRequest request)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id);
        if(reservation == null)
            throw new NotFoundException("Reserva não encontrada.");
        
        if(reservation.Status == StatusReservationEnum.Canceled || reservation.Status == StatusReservationEnum.Completed)
            throw new ErrorOnValidationException("Reserva cancelada ou finalizada.");
        
        var room = await _roomRepository.GetByIdRoomAsync(reservation.IdRoom);
        if(room == null)
            throw new NotFoundException("Quarto não encontrado.");

        switch (request.Status)
        {
            case 1:
                reservation.Status = StatusReservationEnum.Pending;
                break;
            case 2:
                reservation.Status = StatusReservationEnum.Confirmed;
                break;
            case 3:
                reservation.Status = StatusReservationEnum.Canceled;
                break;
            case 4:
                reservation.Status = StatusReservationEnum.Completed;
                break;
            case 5:
                reservation.Status = StatusReservationEnum.NoShow;
                break;
            default:
                throw new ErrorOnValidationException("Status inválido.");
        }

        await _unitOfWork.CommitAsync();
        return reservation.Adapt<ReservationStatusResponse>();
    }
}
