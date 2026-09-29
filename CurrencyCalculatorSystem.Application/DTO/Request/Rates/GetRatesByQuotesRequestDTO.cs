using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.DTO.Request.Rates
{
    public class GetRatesByQuotesRequestDTO
    {
        public IEnumerable<string> quotes { get; set; }
    }
}
