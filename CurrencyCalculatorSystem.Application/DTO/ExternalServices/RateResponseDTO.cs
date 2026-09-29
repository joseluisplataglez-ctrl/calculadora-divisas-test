using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.DTO.ExternalServices
{
    public class RateResponseDTO
    {
        public string date { get; set; } = string.Empty;
        public string @base { get; set; } = string.Empty;
        public string quote { get; set; } = string.Empty;
        public decimal rate { get; set; }
    }
}
