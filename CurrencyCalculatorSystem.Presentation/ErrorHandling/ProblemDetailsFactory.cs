using CurrencyCalculatorSystem.Application.Contracts.ContextApplication;
using CurrencyCalculatorSystem.Application.Exceptions;
using CurrencyCalculatorSystem.Domain.Exception;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyCalculatorSystem.Presentation.ErrorHandling
{
    public static class ProblemDetailsFactory
    {
        public static ProblemDetails Validation(ValidationException ex, HttpContext context, ILocalizer localizer)
        {
            var problem = new ProblemDetails
            {
                Title = "Validation Failed",
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = localizer.GetMessage("DetailMessage"),
                Instance = context.Request.Path
            };

            problem.Extensions["errors"] = ex.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray()
            );
            return problem;
        }

        public static ProblemDetails Domain(DomainException ex, HttpContext context)
        {
            var problem = new ProblemDetails
            {
                Title = "Business Rule Violation",
                Status = StatusCodes.Status422UnprocessableEntity,
                Type = "https://tools.ietf.org/html/rfc4918#section-11.2",
                Detail = ex.Message,
                Instance = context.Request.Path
            };

            problem.Extensions["errorCode"] = ex.ErrorCode;
            return problem;
        }
        public static ProblemDetails NotFound(NotFoundException ex, HttpContext context)
        {
            var problem = new ProblemDetails
            {
                Title = "Not Found",
                Status = StatusCodes.Status404NotFound,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Detail = ex.Message,
                Instance = context.Request.Path
            };
            problem.Extensions["errorCode"] = ex.EntityType != null ? $"Onboarding.{ex.EntityType}.NotFound" : ex.Message;
            return problem;
        }
        public static ProblemDetails Conflict(ConflictException ex, HttpContext context)
        {
            var problem = new ProblemDetails
            {
                Title = "Conflict",
                Status = StatusCodes.Status409Conflict,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                Detail = ex.Message,
                Instance = context.Request.Path
            };
            problem.Extensions["errorCode"] = ex.ErrorCode;
            return problem;
        }
        public static ProblemDetails BadRequest(BadRequestException ex, HttpContext context)
        {
            var problem = new ProblemDetails
            {
                Title = "Bad Request",
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = ex.Message,
                Instance = context.Request.Path
            };
            problem.Extensions["errorCode"] = ex.ErrorCode;
            return problem;
        }
        public static ProblemDetails TooManyOtpAttempts(TooManyAttemptsException ex, HttpContext context)
        {
            var problem = new ProblemDetails
            {
                Title = "Too Many Attempts",
                Status = StatusCodes.Status429TooManyRequests,
                Type = "https://tools.ietf.org/html/rfc6585#section-4",
                Detail = ex.Message,
                Instance = context.Request.Path
            };

            problem.Extensions["errorCode"] = ex.ErrorCode;
            problem.Extensions["maxAttempts"] = ex.MaxAttempts;
            problem.Extensions["retryAfterMinutes"] = ex.WindowMinutes;
            return problem;
        }
        public static ProblemDetails ExternalService(ExternalServiceException ex, HttpContext context)
        {
            // Propagate 4xx from the external service (client error); wrap 5xx as 502 (gateway error)
            var status = ex.StatusCode is >= 400 and < 500
                ? ex.StatusCode.Value
                : StatusCodes.Status502BadGateway;

            var problem = new ProblemDetails
            {
                Title = "External Service Error",
                Status = status,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.3",
                Detail = "El servicio externo no respondió correctamente. Intente de nuevo más tarde.",
                Instance = context.Request.Path
            };

            problem.Extensions["failedService"] = ex.ServiceName;
            problem.Extensions["failedEndpoint"] = ex.Endpoint;

            if (ex.StatusCode.HasValue)
                problem.Extensions["serviceStatusCode"] = ex.StatusCode.Value;

            if (!string.IsNullOrWhiteSpace(ex.Details))
                problem.Extensions["serviceDetail"] = ex.Details;

            return problem;
        }
        public static ProblemDetails Forbidden(ForbiddenException ex, HttpContext context)
        {
            var problem = new ProblemDetails
            {
                Title = "Forbidden",
                Status = StatusCodes.Status403Forbidden,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                Detail = ex.Message,
                Instance = context.Request.Path
            };
            problem.Extensions["errorCode"] = ex.Message;
            return problem;
        }
        public static ProblemDetails Unexpected(Exception ex, HttpContext context)
        {
            return new ProblemDetails
            {
                Title = "Internal Server Error",
                Status = StatusCodes.Status500InternalServerError,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Detail = "An unexpected error occurred. Please try again later.",
                Instance = context.Request.Path
            };
        }
    }
}
