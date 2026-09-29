using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.Exceptions
{
    public class ExternalServiceException(
        string serviceName, string endpoint, int? statusCode = null, string? details = null )
        : Exception(
            $"El servicio externo '{serviceName}' devolvió un error en '{endpoint}'"
            + (statusCode.HasValue ? $" (HTTP {statusCode})" : "")
            + (details is not null ? $":: {details}" : "."))
    {
        public string ServiceName { get; } = serviceName;
        public string Endpoint { get; } = endpoint;
        public int? StatusCode { get; } = statusCode;
        public string? Details { get; } = details;
    }
}
