namespace HotelDashboard.Communication.Responses;

public class ErrorResponse
{
    public List<string> Errors { get; private set; }

    public ErrorResponse(List<string> errorMessages) => Errors = errorMessages;
    public ErrorResponse(string errorMessage) => Errors = new List<string> { errorMessage };
}