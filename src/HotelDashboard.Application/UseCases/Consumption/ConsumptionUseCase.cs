using HotelDashboard.Application.Services.Charges;
using HotelDashboard.Application.Validators.Consumption;
using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;
using HotelDashboard.Domain.Enums;
using HotelDashboard.Domain.Exceptions;
using HotelDashboard.Domain.Repositories;
using Mapster;

namespace HotelDashboard.Application.UseCases.Consumption;

public sealed class ConsumptionUseCase : IConsumptionUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConsumptionRepository _consumptionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IChargeService _chargeService;

    public ConsumptionUseCase(IUnitOfWork unitOfWork, IConsumptionRepository consumptionRepository, IAccountRepository accountRepository, IChargeService chargeService)
    {
        _consumptionRepository = consumptionRepository;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
        _chargeService = chargeService;
    }

    public async Task<ConsumptionResponse> SaveAsync(ConsumptionRequest request)
    {
        var validator = new RegisterConsumptionValidator();
        var result = validator.Validate(request);
        var account = await _accountRepository.GetByIdAsync(request.IdAccount);
        if(account == null)
            throw new NotFoundException("Conta não encontrada.");

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
        
        var consumption = request.Adapt<Domain.Entities.Consumption>();
        consumption.Date = DateTime.Now;
        consumption.Status = StatusConsumptionEnum.Pending;

        await _consumptionRepository.AddAsync(consumption);

        await _chargeService.CreateAsync(new ChargeRequest
        {
            AccountId = consumption.IdAccount,
            ConsumptionId = consumption.Id,
            Type = (int)ChargeTypeEnum.Consumption,
            Description = consumption.Description,
            Quantity = consumption.Quantity,
            UnitAmount = consumption.Value
        });
        await _unitOfWork.CommitAsync();

        return consumption.Adapt<ConsumptionResponse>();
    }

    public async Task<List<ConsumptionResponse>> GetByIdAccountAsync(Guid idAccount)
    {
        var consumptions = await _consumptionRepository.GetByIdListAsync(idAccount);
        if(!consumptions.Any())
            throw new NotFoundException("Nenhum consumo encontrado para a conta informada.");
        
        return consumptions.Adapt<List<ConsumptionResponse>>();
    }
}
