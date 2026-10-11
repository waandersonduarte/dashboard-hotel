namespace HotelDashboard.Domain.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync();
}
