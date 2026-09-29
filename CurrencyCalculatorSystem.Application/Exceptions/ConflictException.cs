using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.Exceptions
{
    public class ConflictException(string errorCode, string detail) : Exception(detail)
    {
        public string ErrorCode { get; } = errorCode;
    }
}
