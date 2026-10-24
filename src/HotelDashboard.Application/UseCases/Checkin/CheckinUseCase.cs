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

    public CheckinUseCase(IUnitOfWork unitOfWork, ICheckinRepository checkinRepository, IRoomRepository roomRepository, IGuestRepository guestRepository)
    {
        _unitOfWork = unitOfWork;
        _checkinRepository = checkinRepository;
        _roomRepository = roomRepository;
        _guestRepository = guestRepository;
    }

    public async Task<CheckinResponse> SaveAsync(CheckinRequest request)
    {
        var validator = new RegisterCheckinValidator();
        var result = validator.Validate(request);

        var existingCheckin = await _checkinRepository.ExistsAsync(request.IdGuest);
        if (existingCheckin)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, "O hospede já possui um checkin registrado."));
        }

        var existingGuest = await _guestRepository.ExistById(request.IdGuest);
        if(!existingGuest)
            throw new NotFoundException("Hóspede não encontrado.");

        var room = await _roomRepository.GetByIdRoomAsync(request.IdRoom);
        if(room == null)
            throw new NotFoundException("Quarto não encontrado.");

        if(room.Status != Domain.Enums.StatusRoomEnum.Available)
            result.Errors.Add(new ValidationFailure(string.Empty, "O quarto informado não está disponível."));

        if(!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }

        var checkin = request.Adapt<Domain.Entities.Checkin>();
        checkin.EntryDate = DateTime.Now;
        room!.Status = Domain.Enums.StatusRoomEnum.Occupied;

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
}
