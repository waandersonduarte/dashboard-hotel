using FluentValidation.Results;
using HotelDashboard.Application.Validators.Checkin;
using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;
using HotelDashboard.Domain.Entities;
using HotelDashboard.Domain.Enums;
using HotelDashboard.Domain.Exceptions;
using HotelDashboard.Domain.Repositories;
using Mapster;

namespace HotelDashboard.Application.UseCases.Checkin;

public sealed class ReservationUseCase : IReservationUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReservationRepository _reservationRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IGuestRepository _guestRepository;

    public ReservationUseCase(IUnitOfWork unitOfWork, IReservationRepository reservationRepository, IRoomRepository roomRepository, IGuestRepository guestRepository)
    {
        _unitOfWork = unitOfWork;
        _reservationRepository = reservationRepository;
        _roomRepository = roomRepository;
        _guestRepository = guestRepository;
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

        var existingGuest = await _guestRepository.ExistById(request.IdGuest);
        if(!existingGuest)
            throw new NotFoundException("Hóspede não encontrado.");

        var room = await _roomRepository.GetByIdRoomAsync(request.IdRoom);
        if(room == null)
            throw new NotFoundException("Quarto não encontrado.");

        if(room.Status != StatusRoomEnum.Available)
            result.Errors.Add(new ValidationFailure(string.Empty, "O quarto informado não está disponível."));

        if(!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }

        var reservation = request.Adapt<Reservation>();
        reservation.Status = StatusReservationEnum.Pending;
        room!.Status = StatusRoomEnum.Reserved;

        await _reservationRepository.AddAsync(reservation);
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
        
        var room = await _roomRepository.GetByIdRoomAsync(reservation.IdRoom);
        if(room == null)
            throw new NotFoundException("Quarto não encontrado.");

        if(room.Status is not (StatusRoomEnum.Available or StatusRoomEnum.Reserved))
            throw new ErrorOnValidationException("O quarto não está disponível para o período informado.");
    
        request.Adapt(reservation);
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
