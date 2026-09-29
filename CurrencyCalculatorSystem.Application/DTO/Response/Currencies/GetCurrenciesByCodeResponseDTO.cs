using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.DTO.Response.Currencies
{
    public class GetCurrenciesByCodeResponseDTO
    {
        public string CurrencyIsoCode { get; private set; } = string.Empty;
        public string CurrencyIsoNumber { get; private set; } = string.Empty;
        public string CurrencyName { get; private set; } = string.Empty;
        public string CurrencySymbol { get; private set; } = string.Empty;
        public DateTime CurrencyStartDate { get; private set; }
    }
}
