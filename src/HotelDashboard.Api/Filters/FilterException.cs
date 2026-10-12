using HotelDashboard.Communication.Responses;
using HotelDashboard.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HotelDashboard.Api.Filters;

public class FilterException : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is HotelDashboardException hotelDashboardException)
        {
            context.HttpContext.Response.StatusCode = (int)hotelDashboardException.GetStatusCode();
            context.Result = new ObjectResult(new ErrorResponse(hotelDashboardException.GetErrorMessages()));
        }
        else
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ErrorResponse("Ocorreu um erro inesperado."));
        }
    }
}
