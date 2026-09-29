using CurrencyCalculatorSystem.Application.Contracts.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace CurrencyCalculatorSystem.Presentation.Localization.Filters
{
    public class LogActionFilterAttribute(
        ISaveLog logService) : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP Desconocida";
            var activity = context.ActionDescriptor.DisplayName ?? "Acción desde el front";
            var method = context.HttpContext.Request.Method;
            var requestParameters = "Sin parámetros";
            if (context.ActionArguments.Count > 0)
            {
                try
                {
                    requestParameters = JsonSerializer.Serialize(context.ActionArguments);
                }
                catch
                {
                    requestParameters = "[Error al serializar parámetros]";
                }
            }

            var fullDetails = $"[IP: {ipAddress}] [Método: {method}] - {activity} | Datos: {requestParameters}";
            logService.SaveLogAsync($"{fullDetails}");
        }

        public override void OnActionExecuted(ActionExecutedContext context) { }
    }
}
