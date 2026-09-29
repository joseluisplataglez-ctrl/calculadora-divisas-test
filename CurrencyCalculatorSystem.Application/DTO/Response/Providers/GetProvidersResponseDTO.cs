using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.DTO.Response.Providers
{
    public class GetProvidersResponseDTO
    {
        public string ProviderKey { get; set; } = string.Empty;
        public string ProviderName { get; set; } = string.Empty;
        public string ProviderCountryCode { get; set; } = string.Empty;
        public string ProviderRateType { get; set; } = string.Empty;
        public string ProviderPivotCurrency { get; set; } = string.Empty;
    }
}
