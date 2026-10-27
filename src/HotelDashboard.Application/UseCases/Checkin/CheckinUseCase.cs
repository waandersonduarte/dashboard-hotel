using FluentValidation.Results;
using HotelDashboard.Application.Validators.Checkin;
using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;
using HotelDashboard.Domain.Enums;
using HotelDashboard.Domain.Exceptions;
using HotelDashboard.Domain.Repositories;
using Mapster;

namespace HotelDashboard.Application.UseCases.Checkin;

public sealed class CheckinUseCase : ICheckinUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICheckinRepository _checkinRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IReservationRepository _reservationRepository;

    public CheckinUseCase(IUnitOfWork unitOfWork, ICheckinRepository checkinRepository, IRoomRepository roomRepository, IGuestRepository guestRepository, IReservationRepository reservationRepository)
    {
        _unitOfWork = unitOfWork;
        _checkinRepository = checkinRepository;
        _roomRepository = roomRepository;
        _guestRepository = guestRepository;
        _reservationRepository = reservationRepository;
    }

    public async Task<CheckinResponse> SaveAsync(CheckinRequest request)
    {
        var reservation = await _reservationRepository.GetByIdAsync(request.IdReservation);
        if(reservation == null)
            // Realiza checkin sem reserva
            return await CheckinWithoutReservation(request);
        
        if(reservation.Status != StatusReservationEnum.Confirmed)
            throw new ErrorOnValidationException("A reserva não foi confirmada.");

        var checkin = new Domain.Entities.Checkin
        {
            IdGuest = reservation.IdGuest,
            IdRoom = reservation.IdRoom,
            EntryDate = DateTime.Now,
            Status = StatusCheckinEnum.Active
        };
        reservation.Room?.Status = StatusRoomEnum.Occupied;

        await _checkinRepository.AddAsync(checkin);
        await _unitOfWork.CommitAsync();

        return checkin.Adapt<CheckinResponse>();
    }

    public async Task<CheckinStatusResponse> UpdateStatusAsync(Guid id, CheckinStatusRequest request)
    {
        var checkin = await _checkinRepository.GetByIdAsync(id);
        if(checkin == null)
            throw new NotFoundException("Checkin não encontrado.");
        
        switch (request.Status)
        {
            case 1:
                checkin.Status = StatusCheckinEnum.Active;
                break;
            case 2:
                checkin.Status = StatusCheckinEnum.Completed;
                break;
            case 3:
                checkin.Status = StatusCheckinEnum.Canceled;
                break;
             case 4:
                checkin.Status = StatusCheckinEnum.Reserved;
                break;
            default:
                throw new ErrorOnValidationException("Status inválido.");
        }
        await _unitOfWork.CommitAsync();
        return checkin.Adapt<CheckinStatusResponse>();
    }

    public async Task<CheckinResponse> GetByIdAsync(Guid id)
    {
        var checkin = await _checkinRepository.GetByIdAsync(id);
        if(checkin == null)
            throw new NotFoundException("Checkin não encontrado.");
        
        return checkin.Adapt<CheckinResponse>();
    }

    public async Task<List<CheckinResponse>> GetByIdGuestAsync(Guid idGuest)
    {
        var checkinGuest = await _checkinRepository.GetListByIdGuestAsync(idGuest);
        if(!checkinGuest.Any())
            throw new NotFoundException("Nenhum checkin encontrado para o hóspede informado.");
        
        return checkinGuest.Adapt<List<CheckinResponse>>();
    }

    public async Task<List<CheckinResponse>> GetAllAsync()
    {
        var checkin = await _checkinRepository.GetAllAsync();
        if(!checkin.Any())
            throw new NotFoundException("Nenhum checkin encontrado.");
        
        return checkin.Adapt<List<CheckinResponse>>();
    }

    private async Task<CheckinResponse> CheckinWithoutReservation(CheckinRequest request)
    {
        var existingCheckin = await _checkinRepository.ExistsCheckinActiveAsync(request.IdGuest);
        if (existingCheckin)
            throw new ErrorOnValidationException("O hospede já possui um checkin ativo.");

        var existingGuest = await _guestRepository.ExistById(request.IdGuest);
        if(!existingGuest)
            throw new NotFoundException("Hóspede não encontrado.");

        var room = await _roomRepository.GetByIdRoomAsync(request.IdRoom);
        if(room == null)
            throw new NotFoundException("Quarto não encontrado.");

        if(room.Status != StatusRoomEnum.Available)
            throw new ErrorOnValidationException("O quarto informado não está disponível.");

        var checkin = new Domain.Entities.Checkin
        {
            IdGuest = request.IdGuest,
            IdRoom = request.IdRoom,
            EntryDate = DateTime.Now,
            Status = StatusCheckinEnum.Active
        };
        room?.Status = StatusRoomEnum.Occupied;

        await _checkinRepository.AddAsync(checkin);
        await _unitOfWork.CommitAsync();

        return checkin.Adapt<CheckinResponse>();
    }
}
