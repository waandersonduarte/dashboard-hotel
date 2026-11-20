using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;
using HotelDashboard.Domain.Entities;
using HotelDashboard.Domain.Enums;
using HotelDashboard.Domain.Exceptions;
using HotelDashboard.Domain.Repositories;
using Mapster;

namespace HotelDashboard.Application.UseCases.Payment;

public sealed class PaymentUseCase : IPaymentUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReservationRepository _reservationRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IConsumptionRepository _consumptionRepository;

    public PaymentUseCase(IUnitOfWork unitOfWork,
        IReservationRepository reservationRepository,
        IPaymentRepository paymentRepository,
        IAccountRepository accountRepository,
        IConsumptionRepository consumptionRepository)
    {
        _unitOfWork = unitOfWork;
        _reservationRepository = reservationRepository;
        _paymentRepository = paymentRepository;
        _accountRepository = accountRepository;
        _consumptionRepository = consumptionRepository;
    }

    public async Task<PaymentResponse> SaveAsync(PaymentRequest request)
    {
        Domain.Entities.Reservation? reservation = null;
        Account? account = null;
        decimal amount = 0;

        if (request.IdReservation.HasValue)
        {
            reservation = await _reservationRepository.GetByIdAsync(request.IdReservation);
            if(reservation == null)
                throw new NotFoundException("Reserva não encontrada.");  
        }

        if (request.IdAccount.HasValue)
        {
            account = await _accountRepository.GetByIdAsync(request.IdAccount);
            if(account == null)
                throw new NotFoundException("Conta não encontrada.");  
        }
        var consumptions = await _consumptionRepository.GetByIdValuePendingListAsync(request.IdAccount);
        amount += consumptions.Sum(c => c.Value * c.Quantity);
        amount += reservation?.TotalAmount ?? 0 + account?.TotalAmount ?? 0;

        if(amount <= 0)
            throw new ErrorOnValidationException("Não há valores pendentes para pagamento.");
        
        var payment = new Domain.Entities.Payment
        {
            IdReservation = reservation?.Id ?? null,
            Amount = amount,
            PaymentDate = DateTime.Now,
            Status = StatusPaymentEnum.Paid
        };

        switch (request.MethodPayment)
        {
            case 1:
                payment.MethodPayment = PaymentMethodEnum.Cash;
                break;
            case 2:
                payment.MethodPayment = PaymentMethodEnum.CreditCard;
                break;
            case 3:
                payment.MethodPayment = PaymentMethodEnum.DebitCard;
                break;
            case 4:
                payment.MethodPayment = PaymentMethodEnum.Pix;
                break;
            case 5:
                payment.MethodPayment = PaymentMethodEnum.BankTransfer;
                break;
            default:
                throw new ErrorOnValidationException("Método de pagamento inválido.");
        }
        reservation?.Status = StatusReservationEnum.Confirmed;
        consumptions.ToList().ForEach(c => c.Status = StatusConsumptionEnum.Invoiced);
        
        await _paymentRepository.AddAsync(payment);
        await _unitOfWork.CommitAsync();

        return payment.Adapt<PaymentResponse>();
    }
}
