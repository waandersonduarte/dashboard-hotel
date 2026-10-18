using FluentValidation.Results;
using HotelDashboard.Application.Validators.Room;
using HotelDashboard.Communication.Requests;
using HotelDashboard.Communication.Responses;
using HotelDashboard.Domain.Enums;
using HotelDashboard.Domain.Exceptions;
using HotelDashboard.Domain.Repositories;
using Mapster;

namespace HotelDashboard.Application.UseCases.Room;

public sealed class RoomUseCase : IRoomUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoomRepository _roomRepository;

    public RoomUseCase(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RoomResponse> SalvarAsync(RoomRequest request)
    {
        var validator = new RegisterRoomValidator();
        var result = validator.Validate(request);

        var existingRoomByNumber = await _roomRepository.ExistByNumberRoomAsync(request.Number);
        if (existingRoomByNumber)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, "Já existe um quarto com o mesmo número."));
        }

        if(!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }

        var room = request.Adapt<Domain.Entities.Room>();
        room.Status = StatusRoomEnum.Available;
        await _roomRepository.AddAsync(room);
        await _unitOfWork.CommitAsync();

        return room.Adapt<RoomResponse>();
    }

    public async Task<List<RoomResponse>> GetAll()
    {
        var rooms = await _roomRepository.GetAllAsync();
        if (rooms.Count == 0)
            throw new NotFoundException("Nenhum quarto encontrado.");
        
        return rooms.Adapt<List<RoomResponse>>();
    }

    public async Task<RoomResponse> GetByNumberRoom(string number)
    {
        var room = await _roomRepository.GetByNumberRoomAsync(number);
        if (room == null)
            throw new NotFoundException("Quarto não encontrado.");
        
        return room.Adapt<RoomResponse>();
    }

    public async Task<RoomStatusResponse> UpdateStatusRoomAsync(Guid id, RoomStatusRequest request)
    {
        var room = await _roomRepository.GetByIdRoomAsync(id);
        if (room == null)
            throw new NotFoundException("Quarto não encontrado.");
        
        switch (request.Status)
        {
            case 1:
                room.Status = StatusRoomEnum.Available;
                break;
            case 2:
                room.Status = StatusRoomEnum.Occupied;
                break;
            case 3:
                room.Status = StatusRoomEnum.Cleaning;
                break;
             case 4:
                room.Status = StatusRoomEnum.Maintenance;
                break;
            default:
                throw new ErrorOnValidationException("Status inválido.");
        }
        await _unitOfWork.CommitAsync();
        return room.Adapt<RoomStatusResponse>();
    }
}