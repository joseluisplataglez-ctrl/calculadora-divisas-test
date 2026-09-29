using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.DTO.ExternalServices
{
    public class ProviderResponseDTO
    {
        public string key { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string country_code { get; set; } = string.Empty;
        public string rate_type { get; set; } = string.Empty;
        public string pivot_currency { get; set; } = string.Empty;
    }
}
