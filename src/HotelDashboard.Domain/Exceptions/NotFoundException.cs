using System.Net;

namespace HotelDashboard.Domain.Exceptions;

public class NotFoundException : HotelDashboardException
{
    private readonly List<string> _errorMessages;
    public NotFoundException(List<string> errorMessages)
    {
        _errorMessages = errorMessages;
    }
    public NotFoundException(string errorMessage)
    {
        _errorMessages = new List<string> { errorMessage };
    }

    public override List<string> GetErrorMessages() => _errorMessages;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
}
