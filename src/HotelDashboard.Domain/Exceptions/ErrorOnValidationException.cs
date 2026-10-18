using System.Net;

namespace HotelDashboard.Domain.Exceptions;

public class ErrorOnValidationException : HotelDashboardException
{
    private readonly List<string> _errorMessages;
    public ErrorOnValidationException(List<string> errorMessages)
    {
        _errorMessages = errorMessages;
    }

    public ErrorOnValidationException(string errorMessage)
    {
        _errorMessages = new List<string> { errorMessage };
    }

    public override List<string> GetErrorMessages() => _errorMessages;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}
