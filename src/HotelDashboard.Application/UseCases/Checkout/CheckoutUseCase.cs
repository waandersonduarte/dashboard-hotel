using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;
using HotelDashboard.Domain.Enums;
using HotelDashboard.Domain.Exceptions;
using HotelDashboard.Domain.Repositories;

namespace HotelDashboard.Application.UseCases.Checkout;

public sealed class CheckoutUseCase : ICheckoutUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICheckoutRepository _checkoutRepository;
    private readonly ICheckinRepository _checkinRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IChargeRepository _chargeRepository;
    private readonly IRoomRepository _roomRepository;

    public CheckoutUseCase(ICheckoutRepository checkoutRepository,
    ICheckinRepository checkinRepository,
    IAccountRepository accountRepository,
    IChargeRepository chargeRepository,
    IUnitOfWork unitOfWork,
    IRoomRepository roomRepository)
    {
        _checkoutRepository = checkoutRepository;
        _checkinRepository = checkinRepository;
        _accountRepository = accountRepository;
        _chargeRepository = chargeRepository;
        _unitOfWork = unitOfWork;
        _roomRepository = roomRepository;
    }

    public async Task<CheckoutResponse> ExecuteAsync(CheckoutRequest request)
    {
        var checkin = await _checkinRepository.GetByIdAsync(request.IdCheckin);
        if (checkin == null)
            throw new NotFoundException("Check-in não encontrado.");

        if (checkin.Status != StatusCheckinEnum.Active)
            throw new ErrorOnValidationException("O check-in não está ativo.");
        
        if(await _checkoutRepository.ExistsCheckoutAsync(request.IdCheckin))
            throw new ErrorOnValidationException("O check-out já foi realizado para este check-in.");
        
        var account = await _accountRepository.GetByIdCheckinIncludeConsumptionChargesAsync(request.IdCheckin);
        if (account == null)
            throw new NotFoundException("Conta não encontrada para este check-in.");
        
        if (account.Status != StatusAccountEnum.Open)
            throw new ErrorOnValidationException("A conta não está aberta.");
        
        var charges = await _chargeRepository.GetByAccountIdWithAllocationsAsync(account.Id);
        var room = await _roomRepository.GetByIdRoomAsync(checkin.IdRoom.Value);
                
        var pendingCharges = charges.Select(charge =>
        {
            var totalPaid = charge.Allocations
                .Where(x => 
                    x.Payment.Status == StatusPaymentEnum.Paid)
                .Sum(x => x.Amount);
            var remainingAmount = charge.Amount - totalPaid;
            return new
            {
                Charge = charge,
                RemainingAmount = remainingAmount
            };
        })
        .Where(charge => charge.RemainingAmount > 0)
        .ToList();

        if (pendingCharges.Any())
            throw new ErrorOnValidationException("Existem cobranças pendentes na conta. Efetue o pagamento antes de realizar o check-out.");
        
        var checkout = new Domain.Entities.Checkout
        {
            IdCheckin = request.IdCheckin,
            CheckoutDate = DateTime.UtcNow
        };
        await _checkoutRepository.AddAsync(checkout);

        account.Status = StatusAccountEnum.Closed;
        account.DateClosing = DateTime.UtcNow;
        checkin.Status = StatusCheckinEnum.Completed;
        room!.Status = StatusRoomEnum.Cleaning;

        await _unitOfWork.CommitAsync();

        return new CheckoutResponse
        {
            Id = checkout.Id,
            IdCheckin = checkout.IdCheckin,
            DateCheckout = checkout.CheckoutDate,
            CheckInStatus = checkin.Status.ToString(),
            AccountStatus = account.Status.ToString(),
            RoomStatus = checkin.Room.Status.ToString()
        };
    }
}
