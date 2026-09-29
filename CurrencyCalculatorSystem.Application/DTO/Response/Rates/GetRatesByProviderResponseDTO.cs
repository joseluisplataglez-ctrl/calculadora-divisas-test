using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.DTO.Response.Rates
{
    public class GetRatesByProviderResponseDTO
    {
        public DateTime CurrencyDate { get; private set; }
        public string CurrencyBase { get; private set; } = string.Empty;
        public string QuoteCurrency { get; private set; } = string.Empty;
        public decimal RateValue { get; private set; }
        public List<ProvidersDTO> Providers { get; set; } = new();
    }

    public class ProvidersDTO
    {
        public string ProviderKey { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Rate { get; set; }
    }
}
