using CurrencyCalculatorSystem.Application.Contracts.ContextApplication;
using CurrencyCalculatorSystem.Application.Resources;
using CurrencyCalculatorSystem.Domain.Exception;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Infrastructure.Services.Localization
{
    public class Localizer(
        IStringLocalizer<ValidationMessages> validationLocalizer,
        IStringLocalizer<LoggerMessages> loggerLocalizer,
        IStringLocalizer<ExceptionMessages> exceptionLocalizer,
        IStringLocalizer<ResponseMessages> responseLocalizer,
        IStringLocalizer<DomainConcepts> domainConceptLocalizer,
        IStringLocalizer<EnumValues> enumValuesLocalizer)
        : ILocalizer
    {
        /// <summary>
        /// Mapea los codigos de error a mensajes localizados. 
        /// Si el tipo de excepción no es reconocido, devuelve un mensaje de error inesperado.
        /// </summary>
        /// <param name="ex">Excepción</param>
        /// <returns>Mensaje localizado</returns>
        public string GetMessage(Exception ex)
        {
            return ex switch
            {
                DomainException de => domainConceptLocalizer[de.Message],
                FluentValidation.ValidationException ve => validationLocalizer[ve.Message],
                _ => responseLocalizer["UnexpectedError"]
            };
        }

        /// <summary>
        /// Obtiene un mensaje localizado basado en la clave proporcionada para API Response.
        /// Si la clave es nula, devuelve un mensaje de éxito.
        /// </summary>
        /// <param name="key">Clave del mensaje</param>
        /// <param name="args">Argumentos para formatear el mensaje</param>
        /// <returns>Mensaje localizado</returns>
        public string GetMessage(string? key, params object[] args)
        {
            if (key == null)
                return "Success";

            return responseLocalizer[key, args];
        }
    }
}
