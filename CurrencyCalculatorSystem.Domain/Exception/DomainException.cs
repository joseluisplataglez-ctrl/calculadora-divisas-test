

namespace CurrencyCalculatorSystem.Domain.Exception;

public class DomainException(string messageKey, params object[]? args) : IOException(messageKey)
{
    public object[]? Args { get; } = args;
    public string ErrorCode { get; } = messageKey;
}
