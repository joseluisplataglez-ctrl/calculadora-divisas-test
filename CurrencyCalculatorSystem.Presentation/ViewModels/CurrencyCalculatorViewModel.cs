using CurrencyCalculatorSystem.Presentation.ViewModels.Currencies;

namespace CurrencyCalculatorSystem.Presentation.ViewModels
{
    public class CurrencyCalculatorViewModel
    {
        public List<CurrencyViewModel> CurrenciesAvailables { get; set; } = new();

        public string CurrencyBase { get; set; } = string.Empty;
        public decimal Qty { get; set; }
        public decimal Result { get; set; }
    }    
}
