using CurrencyCalculatorSystem.Application.DTO.ExternalServices;
using CurrencyCalculatorSystem.Application.DTO.Response.Currencies;
using CurrencyCalculatorSystem.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.Contracts.Services
{
    public interface IFrankfurter
    {
        #region Rates

        Task<List<RateResponseDTO>> GetLatestRatesAsync(CancellationToken ct);
        Task<List<RateResponseDTO>> GetRatesByBaseAsync(string baseRate, CancellationToken ct);
        Task<List<RateResponseDTO>> GetRatesByQuotesAsync(IEnumerable<string> quotes, CancellationToken ct);
        Task<List<RateResponseDTO>> GetRatesByDateAsync(DateTime date, CancellationToken ct);
        Task<List<RateResponseDTO>> GetRatesByPeriodAndQuoteAsync(DateTime date, string quote, CancellationToken ct);
        Task<List<RateResponseDTO>> GetRatesGroupByPeriodAsync(DateTime date, string typeGroup, CancellationToken ct);
        Task<List<RateResponseDTO>> GetRatesByProviderAsync(string provider, CancellationToken ct);
        Task<List<RateResponseDTO>> GetRatesByProvidersAsync(CancellationToken ct);
        Task<RateResponseDTO> GetRateByPairCurrenciesDateAsync(string baseCurrency, string quoteCurrency,string date, CancellationToken ct);

        #endregion

        #region Currencies

        Task<List<CurrencyResponseDTO>> GetCurrenciesAsync(CancellationToken ct);
        Task<List<CurrencyResponseDTO>> GetLegacyCurrenciesAsync(CancellationToken ct);
        Task<List<CurrencyResponseDTO>> GetCurrenciesByCodeAsync(string code, CancellationToken ct);

        #endregion

        #region Providers

        Task<List<ProviderResponseDTO>> GetProvidersAsync(CancellationToken ct);
        Task<ProviderResponseDTO> GetProviderDetailAsync(string providerKey, CancellationToken ct);
        

        #endregion
    }
}
