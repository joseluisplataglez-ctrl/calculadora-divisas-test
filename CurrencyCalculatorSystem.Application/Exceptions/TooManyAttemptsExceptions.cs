using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.Exceptions
{
    public class TooManyAttemptsException(int maxAttempts, int windowMinutes)
    : Exception(
        $"Se ha excedido el número máximo de intentos OTP ({maxAttempts}). " +
        $"Intente de nuevo en {windowMinutes} minuto(s).")
    {
        public string ErrorCode => "Otp.MaxAttemptsExceeded";
        public int MaxAttempts { get; } = maxAttempts;

        /// <summary>Minutos que debe esperar el usuario antes de poder reintentar.</summary>
        public int WindowMinutes { get; } = windowMinutes;
    }
}
