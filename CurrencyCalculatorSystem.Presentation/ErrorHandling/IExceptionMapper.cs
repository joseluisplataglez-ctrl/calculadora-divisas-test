using Microsoft.AspNetCore.Mvc;

namespace CurrencyCalculatorSystem.Presentation.ErrorHandling
{
    public interface IExceptionMapper
    {
        bool CanHandle(Exception exception);
        ProblemDetails MapToProblemDetails(Exception exception, HttpContext context);
    }
}
