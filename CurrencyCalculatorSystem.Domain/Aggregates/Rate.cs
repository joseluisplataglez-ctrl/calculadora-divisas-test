using CurrencyCalculatorSystem.Domain.Common;
using CurrencyCalculatorSystem.Domain.Exception;

namespace CurrencyCalculatorSystem.Domain.Aggregates
{
    public class Rate : BaseDomainModel
    {
        public DateTime CurrencyDate { get; private set; }
        public decimal RateValue { get; private set; }

        public int ProviderId { get; private set; } 
        public virtual Provider Provider { get; private set; }

        public int CurrencyBaseId { get; private set; }
        public virtual Currency CurrencyBase { get; private set; }

        public int QuoteCurrencyId { get; private set; }
        public virtual Currency QuoteCurrency { get; private set; }

        private Rate() { }

        public Rate(DateTime currencyDate, int providerId, int currencyBaseId, int quoteCurrencyId, decimal rateValue)
        {            
            if (providerId <= 0)
                throw new DomainException("Un proveedor válido es requerido.");

            if (currencyBaseId <= 0 || quoteCurrencyId <= 0)
                throw new DomainException("Los IDs de las monedas base y destino deben ser válidos.");

            if (currencyBaseId == quoteCurrencyId)
                throw new DomainException("La moneda base y la moneda destino no pueden ser la misma.");

            if (rateValue < 0)
                throw new DomainException("La tasa de cambio no puede ser un valor negativo.");

            CurrencyDate = currencyDate;
            ProviderId = providerId;
            CurrencyBaseId = currencyBaseId;
            QuoteCurrencyId = quoteCurrencyId;
            RateValue = rateValue;
        }
    }
}
