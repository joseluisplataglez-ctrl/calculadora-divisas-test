using CurrencyCalculatorSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Domain.Aggregates
{
    public class Currency :BaseDomainModel
    {
        public string CurrencyIsoCode       { get; private set; } = string.Empty;
        public string CurrencyIsoNumber     { get; private set; } = string.Empty;
        public string CurrencyName          { get; private set; } = string.Empty;
        public string CurrencySymbol        { get; private set; } = string.Empty;
        public DateTime CurrencyStartDate   { get; private set; }

        public virtual ICollection<Rate> BaseRates { get; set; } = new List<Rate>();
        public virtual ICollection<Rate> QuoteRates { get; set; } = new List<Rate>();

        private Currency() { }
    }
}
