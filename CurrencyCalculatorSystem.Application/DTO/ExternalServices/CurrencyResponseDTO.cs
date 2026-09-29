using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.DTO.ExternalServices
{
    public class CurrencyResponseDTO
    {
        public string iso_code { get; set; } = string.Empty;
        public string iso_numeric { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string start_date { get; set; } = string.Empty;
        public List<ProviderResponseDTO> providers { get; set; } = new();
    }
}
