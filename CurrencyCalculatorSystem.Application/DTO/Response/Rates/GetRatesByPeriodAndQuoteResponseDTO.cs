using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.DTO.Response.Rates
{
    public class GetRatesByPeriodAndQuoteResponseDTO
    {
        public DateTime CurrencyDate { get; private set; }
        public string CurrencyBase { get; private set; } = string.Empty;
        public string QuoteCurrency { get; private set; } = string.Empty;
        public decimal RateValue { get; private set; }
    }
}
