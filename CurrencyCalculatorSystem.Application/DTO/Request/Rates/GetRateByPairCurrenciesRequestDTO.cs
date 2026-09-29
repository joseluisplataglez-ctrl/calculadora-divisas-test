using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.DTO.Request.Rates
{
    public class GetRateByPairCurrenciesRequestDTO
    {
        public string baseCurrency { get; set; } =string.Empty;
        public string quoteCurrency { get; set; } = string.Empty;
        public string date { get; set; } = string.Empty;
    }
}
