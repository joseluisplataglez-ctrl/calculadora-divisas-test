namespace CurrencyCalculatorSystem.Presentation.ViewModels.Rates
{
    public class RateViewModel
    {
        public DateTime CurrencyDate { get; private set; }
        public string CurrencyBase { get; private set; } = string.Empty;
        public string QuoteCurrency { get; private set; } = string.Empty;
        public decimal RateValue { get; private set; }
    }
}
