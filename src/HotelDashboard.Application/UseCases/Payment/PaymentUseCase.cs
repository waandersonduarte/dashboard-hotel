using HotelDashboard.Application.Services.Reservations;
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
    private readonly IPaymentRepository _paymentRepository;
    private readonly IChargeRepository _chargeRepository;
    private readonly IReservationConfirmationService _reservationConfirmationService;

    public PaymentUseCase(IUnitOfWork unitOfWork,
        IPaymentRepository paymentRepository,
        IChargeRepository chargeRepository,
        IReservationConfirmationService reservationConfirmationService)
    {
        _unitOfWork = unitOfWork;
        _paymentRepository = paymentRepository;
        _chargeRepository = chargeRepository;
        _reservationConfirmationService = reservationConfirmationService;
    }

    public async Task<PaymentResponse> SaveAsync(PaymentRequest request)
    {
        if(request.Amount <= 0)
            throw new ErrorOnValidationException("O valor do pagamento deve ser maior que zero.");

        if (request.Allocations == null || !request.Allocations.Any())
            throw new ErrorOnValidationException("Informe pelo menos uma cobrança.");
        
        if (request.Allocations.Any(x => x.Amount <= 0))
            throw new ErrorOnValidationException("Os valores alocados devem ser positivos.");
        
        if (request.Allocations.GroupBy(x => x.ChargeId).Any(x => x.Count() > 1))
            throw new ErrorOnValidationException("Existem cobranças duplicadas no pagamento.");

        var totalAllocated = request.Allocations.Sum(x => x.Amount);
        if (totalAllocated != request.Amount)
            throw new ErrorOnValidationException("A soma das alocações deve ser igual ao pagamento.");
        
        
        var payment = new Domain.Entities.Payment
        {
            Id = Guid.NewGuid(),
            Amount = request.Amount,
            MethodPayment = MapPaymentMethod(request.MethodPayment),
            Status = StatusPaymentEnum.Paid,
            PaymentDate = DateTime.UtcNow
        };

        var reservationsIds = new HashSet<Guid>();

        foreach (var allocation in request.Allocations)
        {
            var charge = await _chargeRepository.GetWithPaymentsByIdAsync(allocation.ChargeId);
            if (charge == null)
                throw new NotFoundException($"Cobrança com ID {allocation.ChargeId} não encontrada.");

            var totalPaid = charge.Allocations
                .Where(x => 
                    x.Payment.Status == StatusPaymentEnum.Paid)
                .Sum(x => x.Amount);

            var remainingAmount = charge.Amount - totalPaid;
            if (allocation.Amount > remainingAmount)
                throw new ErrorOnValidationException($"O pagamento ultrapassa o saldo da cobrança {allocation.ChargeId}.");

            payment.Allocations.Add(new PaymentAllocation
            {
                Id = Guid.NewGuid(),
                IdCharge = charge.Id,
                Amount = allocation.Amount
            });

            if(charge.IdReservation.HasValue)
                reservationsIds.Add(charge.IdReservation.Value);
        }
        
        await _paymentRepository.AddAsync(payment);
        await _unitOfWork.CommitAsync();

        foreach (var reservationId in reservationsIds)
        {
            await _reservationConfirmationService.ReservationConfirmationServiceAsync(reservationId);
        }
        await _unitOfWork.CommitAsync();

        return payment.Adapt<PaymentResponse>();
    }

    public async Task<ChargePendingResponse> ChargePendingAsync(GetPendingChargesRequest request)
    {
        List<Charge> charges;

        if(request.IdAccount.HasValue && request.IdReservation.HasValue)
            throw new ErrorOnValidationException("Informe apenas o Id da conta ou da reserva, não ambos.");
        
        if(request.IdAccount.HasValue)
            charges = await _chargeRepository.GetByAccountIdWithAllocationsAsync(request.IdAccount.Value);
        else if(request.IdReservation.HasValue)
            charges = await _chargeRepository.GetByReservationIdWithAllocationsAsync(request.IdReservation.Value);
        else
            throw new ErrorOnValidationException("Informe o Id da conta ou da reserva para buscar as cobranças pendentes.");

        var pendingCharges = charges.Select(charge =>
        {
            var totalPaid = charge.Allocations
                .Where(x => 
                    x.Payment.Status == StatusPaymentEnum.Paid)
                .Sum(x => x.Amount);

            return new ChargeResponse
            {
                IdCharge = charge.Id,
                Description = charge.Description,
                Amount = charge.Amount,
                Type = charge.Type.ToString(),
                PaidAmount = totalPaid,
                RemainingAmount = charge.Amount - totalPaid
            };
        })
        .Where(charge => charge.RemainingAmount > 0)
        .ToList();

        if(!pendingCharges.Any())
            throw new NotFoundException("Não existem cobranças pendentes para o Id informado.");

        return new ChargePendingResponse
        {
            TotalPending = pendingCharges.Sum(x => x.RemainingAmount),
            Charges = pendingCharges
        };
    }

    private PaymentMethodEnum MapPaymentMethod(int methodPayment)
    {
        return methodPayment switch
        {
            1 => PaymentMethodEnum.Cash,
            2 => PaymentMethodEnum.CreditCard,
            3 => PaymentMethodEnum.DebitCard,
            4 => PaymentMethodEnum.Pix,
            5 => PaymentMethodEnum.BankTransfer,
            _ => throw new ErrorOnValidationException("Método de pagamento inválido.")
        };
    }
}
