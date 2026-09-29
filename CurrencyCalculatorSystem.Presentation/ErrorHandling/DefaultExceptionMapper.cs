using CurrencyCalculatorSystem.Application.Contracts.ContextApplication;
using CurrencyCalculatorSystem.Application.Exceptions;
using CurrencyCalculatorSystem.Domain.Exception;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;


namespace CurrencyCalculatorSystem.Presentation.ErrorHandling
{
    public class DefaultExceptionMapper(ILocalizer localizer) : IExceptionMapper
    {
        public bool CanHandle(Exception exception) => true;

        public ProblemDetails MapToProblemDetails(Exception exception, HttpContext context)
        {
            return exception switch
            {
                ValidationException ve => ProblemDetailsFactory.Validation(ve, context, localizer),
                DomainException de => ProblemDetailsFactory.Domain(de, context),
                NotFoundException nfe => ProblemDetailsFactory.NotFound(nfe, context),
                ConflictException ce => ProblemDetailsFactory.Conflict(ce, context),
                BadRequestException bre => ProblemDetailsFactory.BadRequest(bre, context),
                TooManyAttemptsException tme => ProblemDetailsFactory.TooManyOtpAttempts(tme, context),
                ExternalServiceException ese => ProblemDetailsFactory.ExternalService(ese, context),
                ForbiddenException fe => ProblemDetailsFactory.Forbidden(fe, context),
                _ => ProblemDetailsFactory.Unexpected(exception, context)
            };
        }
    }
}
