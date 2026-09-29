using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.DTO.Request.Rates
{
    public class GetRatesByPeriodAndQuoteRequestDTO
    {
        public DateTime date { get; set; }
        public string quote { get; set; } = string.Empty;
    }
}
