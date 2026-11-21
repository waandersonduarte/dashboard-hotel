using HotelDashboard.Communication.Requests;
using HotelDashboard.Domain.Entities;

namespace HotelDashboard.Application.Services.Charges;

public interface IChargeService
{
    Task CreateAsync(ChargeRequest request);
}
