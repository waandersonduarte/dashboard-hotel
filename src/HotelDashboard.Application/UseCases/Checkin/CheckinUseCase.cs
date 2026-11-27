using HotelDashboard.Application.Services.Charges;
using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;
using HotelDashboard.Domain.Entities;
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
    private readonly IAccountRepository _accountRepository;
    private readonly IChargeRepository _chargeRepository;
    private readonly IChargeService _chargeService;

    public CheckinUseCase(IUnitOfWork unitOfWork,
        ICheckinRepository checkinRepository,
        IRoomRepository roomRepository,
        IGuestRepository guestRepository,
        IReservationRepository reservationRepository,
        IAccountRepository accountRepository,
        IChargeRepository chargeRepository,
        IChargeService chargeService)
    {
        _unitOfWork = unitOfWork;
        _checkinRepository = checkinRepository;
        _roomRepository = roomRepository;
        _guestRepository = guestRepository;
        _reservationRepository = reservationRepository;
        _accountRepository = accountRepository;
        _chargeRepository = chargeRepository;
        _chargeService = chargeService;
    }

    public async Task<CheckinResponse> SaveAsync(CheckinRequest request)
    {
        var reservation = await _reservationRepository.GetByIdAsync(request.IdReservation);
        if(reservation == null)
            // Realiza checkin sem reserva
            return await CheckinWithoutReservation(request);
        
        if(reservation.Status != StatusReservationEnum.Confirmed)
            throw new ErrorOnValidationException("A reserva não foi confirmada.");

        var charge = await _chargeRepository.GetAccommodationByReservationIdAsync(reservation.Id);
        if (charge == null)
            throw new NotFoundException("Cobrança da hospedagem não encontrada.");

        if (charge.IdAccount.HasValue)
            throw new ErrorOnValidationException("Esta cobrança já está associada a uma conta.");
        
        if(reservation.CheckinDate > DateTime.UtcNow)
            throw new ErrorOnValidationException("O check-in não pode ser realizado antes da data de entrada da reserva.");

        if(reservation.CheckoutDate < DateTime.UtcNow)
            throw new ErrorOnValidationException("O check-in não pode ser realizado após a data de saída da reserva.");
        
        if(await _checkinRepository.ExistsCheckinActiveAsync(reservation.IdGuest))
            throw new ErrorOnValidationException("O hóspede já possui um check-in ativo.");

        var isAvailable = await _roomRepository.IsAvailableAsync(
            reservation.IdRoom,
            reservation.CheckinDate,
            reservation.CheckoutDate,
            reservation.Id);
        if(!isAvailable)
            throw new ErrorOnValidationException("O quarto informado não está disponível.");


        var checkin = request.Adapt<Domain.Entities.Checkin>();
        checkin.IdGuest = reservation.IdGuest;
        checkin.IdRoom = reservation.IdRoom;
        checkin.CheckinDate = DateTime.UtcNow;
        checkin.CheckoutDate = reservation.CheckoutDate;
        checkin.Status = StatusCheckinEnum.Active;
        
        reservation.Room?.Status = StatusRoomEnum.Occupied;
        reservation.Status = StatusReservationEnum.CheckedIn;
        await _checkinRepository.AddAsync(checkin);

        var account = await CreateAccount(checkin.Id);
        charge.IdAccount = account.Id;

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
        if(request.IdGuest == null)
            throw new ErrorOnValidationException("O hóspede deve ser informado para o checkin sem reserva.");
        else
        {
            var existingCheckin = await _checkinRepository.ExistsCheckinActiveAsync(request.IdGuest);
            if (existingCheckin)
                throw new ErrorOnValidationException("O hospede já possui um checkin ativo.");

            var existingGuest = await _guestRepository.ExistById(request.IdGuest);
            if(!existingGuest)
                throw new NotFoundException("Hóspede não encontrado.");
        }

        var room = await _roomRepository.GetByIdRoomAsync(request.IdRoom);
        if(room == null)
            throw new NotFoundException("Quarto não encontrado.");

        if(request.IdRoom.HasValue && request.CheckinDate.HasValue && request.CheckoutDate.HasValue)
        {
            var isAvailable = await _roomRepository.IsAvailableAsync(
                request.IdRoom.Value,
                request.CheckinDate.Value,
                request.CheckoutDate.Value);
            if(!isAvailable)
                throw new ErrorOnValidationException("O quarto informado não está disponível.");
        }
        

        if(!request.IdReservation.HasValue && request.CheckinDate >= request.CheckoutDate)
            throw new ErrorOnValidationException("A data de check-in deve ser anterior à data de check-out.");
        
        if(!request.IdReservation.HasValue && !request.CheckinDate.HasValue)
            throw new ErrorOnValidationException("A data de check-in deve ser informada.");
        
         if(!request.IdReservation.HasValue && !request.CheckoutDate.HasValue)
            throw new ErrorOnValidationException("A data de check-out deve ser informada.");
        
        var guest = await _guestRepository.GetByIdAsync((Guid)request.IdGuest!);
        var checkin = request.Adapt<Domain.Entities.Checkin>();
        checkin.Status = StatusCheckinEnum.Active;
        
        room.Status = StatusRoomEnum.Occupied;
        await _checkinRepository.AddAsync(checkin);

        var account = await CreateAccount(checkin.Id);
        var totalDays = (checkin.CheckoutDate.Date - checkin.CheckinDate.Date).Days;

        await _chargeService.CreateAsync(new ChargeRequest
        {
            AccountId = account.Id,
            Type = (int)ChargeTypeEnum.Accommodation,
            Description = $"Cobrança referente ao check-in do quarto {room.Number} para o hóspede {guest!.Name}.",
            Quantity = totalDays,
            UnitAmount = room.DailyRate
        });

        await _unitOfWork.CommitAsync();
        return checkin.Adapt<CheckinResponse>();
    }

    private async Task<Account> CreateAccount(Guid idCheckin)
    {
        var account = new Account
        {
            IdCheckin = idCheckin,
            Status = StatusAccountEnum.Open
        };

        await _accountRepository.AddAsync(account);
        return account;
    }
}
