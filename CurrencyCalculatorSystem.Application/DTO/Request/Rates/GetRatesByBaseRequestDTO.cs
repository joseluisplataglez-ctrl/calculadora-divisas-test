using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.DTO.Request.Rates
{
    public class GetRatesByBaseRequestDTO
    {
        public string baseRate { get; set; } = string.Empty;
    }
}
