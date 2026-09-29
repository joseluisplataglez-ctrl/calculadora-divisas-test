using CurrencyCalculatorSystem.Application.Contracts.ContextApplication;
using CurrencyCalculatorSystem.Domain.Exception;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.Exceptions
{
    public static partial class Utility
    {
        [GeneratedRegex(@"(\w*.\w*:\w* \d*)")]
        private static partial Regex ErrorMessagesRegex();

        public static string ParseStackTrace(Exception ex)
        {
            if (ex == null)
                return string.Empty;

            string[] stackTraceToArrayString = [.. ErrorMessagesRegex().Matches(ex.StackTrace!).ToArray().Select(x => x.Value)];

            return string.Join(" ->", stackTraceToArrayString);
        }

        public static string ExceptionMessages(Exception ex, ILocalizer localizer = null!)
        {
            if (ex == null)
                return string.Empty;

            if (ex is DomainException domainException)
                return localizer != null
                    ? localizer.GetMessage(domainException.Message, domainException.Args ?? [])
                    : domainException.Message;

            if (ex is NotFoundException notFoundException)
                return localizer != null
                    ? localizer.GetMessage("NotFound", localizer.GetMessage(notFoundException.EntityType), notFoundException.Id.ToString())
                    : notFoundException.Message;

            if (ex is ValidationException validationException)
                return string.Join("\n", validationException.Errors.Select(e => $"- {e.ErrorMessage}"));

            if (ex.InnerException == null)
                return ex.Message;

            return ex.Message + " -> " + ExceptionMessages(ex.InnerException);
        }

    }
}
