using CurrencyCalculatorSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Domain.Aggregates
{
    public class FavouriteCurrencies : BaseDomainModel
    {
        public string FavouriteCurrency { get; set; } = string.Empty;
    }
}
