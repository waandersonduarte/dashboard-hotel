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

    public PaymentUseCase(IUnitOfWork unitOfWork,
        IReservationRepository reservationRepository,
        IPaymentRepository paymentRepository,
        IAccountRepository accountRepository)
    {
        _unitOfWork = unitOfWork;
        _reservationRepository = reservationRepository;
        _paymentRepository = paymentRepository;
        _accountRepository = accountRepository;
    }

    public async Task<PaymentResponse> SaveAsync(PaymentRequest request)
    {
        Domain.Entities.Reservation? reservation = null;
        Account? account = null;

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

        var amount = reservation?.TotalAmount ?? 0 + account?.TotalAmount ?? 0;
        
        var payment = new Domain.Entities.Payment
        {
            IdReservation = reservation?.Id ?? null,
            Amount = amount,
            PaymentDate = DateTime.Now,
            Status = StatusPaymentEnum.Pending
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
                throw new ErrorOnValidationException("Status inválido.");
        }
        reservation?.Status = StatusReservationEnum.Confirmed;
        await _paymentRepository.AddAsync(payment);
        await _unitOfWork.CommitAsync();

        return payment.Adapt<PaymentResponse>();
    }
}
