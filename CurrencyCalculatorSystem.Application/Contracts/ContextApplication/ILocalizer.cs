namespace CurrencyCalculatorSystem.Application.Contracts.ContextApplication
{
    public interface ILocalizer
    {
        string GetMessage(Exception ex);
        string GetMessage(string? key, params object[] args);
    }
}
