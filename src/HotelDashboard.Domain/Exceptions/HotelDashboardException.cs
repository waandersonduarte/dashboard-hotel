using System.Net;

namespace HotelDashboard.Domain.Exceptions;

public abstract class HotelDashboardException : Exception
{
    public abstract HttpStatusCode GetStatusCode();
    public abstract List<string> GetErrorMessages();
}
