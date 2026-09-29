using CurrencyCalculatorSystem.Domain.Common;
using CurrencyCalculatorSystem.Domain.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Domain.Aggregates
{
    public class Provider : BaseDomainModel
    {
        public string ProviderKey           { get; set; } = string.Empty;
        public string ProviderName          { get; set; } = string.Empty;
        public string ProviderCountryCode   { get; set; } = string.Empty;
        public string ProviderRateType      { get; set; } = string.Empty;
        public string ProviderPivotCurrency { get; set; } = string.Empty;

        public virtual ICollection<Rate> Rates { get; set; } = new List<Rate>();

        private Provider() { }

        public Provider(
            string providerKey, string providerName, string providerCountryCode,
            string providerRateType, string providerPivotCurrency)
        {
            if (string.IsNullOrEmpty(providerKey))
                throw new DomainException("Provider Key is required");

            ProviderKey             = providerKey;
            ProviderName            = providerName;
            ProviderCountryCode     = providerCountryCode;
            ProviderRateType        = providerRateType;
            ProviderPivotCurrency   = providerPivotCurrency;
        }
    }
}
