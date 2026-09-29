using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.DTO.Common
{
    public class ExternalServiceConfiguration
    {
        public FrankfurterSettings Frankfurter { get; set; } = new();
    }

    public class FrankfurterSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
    }
}
