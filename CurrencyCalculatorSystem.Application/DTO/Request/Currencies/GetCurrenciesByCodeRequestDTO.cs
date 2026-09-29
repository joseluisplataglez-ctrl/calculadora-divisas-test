using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.DTO.Request.Currencies
{
    public class GetCurrenciesByCodeRequestDTO
    {
        public string providerCode { get; set; } = string.Empty;
    }
}
