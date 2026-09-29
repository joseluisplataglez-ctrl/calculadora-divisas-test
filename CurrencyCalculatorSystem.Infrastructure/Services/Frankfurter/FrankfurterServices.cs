using CurrencyCalculatorSystem.Application.Contracts.Services;
using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.ExternalServices;
using CurrencyCalculatorSystem.Application.DTO.Response.Currencies;
using CurrencyCalculatorSystem.Domain.Aggregates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Infrastructure.Services.Frankfurter
{
    public class FrankfurterServices(
        IHttpClientFactory clientFactory,
        IOptions<ExternalServiceConfiguration> settings,
        ILogger<FrankfurterServices> logger,
        IConfiguration configuration
        ) : IFrankfurter
    {
        private readonly FrankfurterSettings _config = settings.Value.Frankfurter;
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        private string rootPath => configuration["Services:Frankfurter:baseUrl"] ?? string.Empty;

        #region Currencies

        public async Task<List<CurrencyResponseDTO>> GetCurrenciesAsync(CancellationToken ct)
        {
            List<CurrencyResponseDTO> lstResult = new List<CurrencyResponseDTO>();
            var shortcutUrl = $"{rootPath}/currencies";
            var httpClientShort = clientFactory.CreateClient("FrankfurterClient");
            HttpResponseMessage response = await httpClientShort.GetAsync(shortcutUrl,ct);

            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync(ct);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var deserializedResult = await JsonSerializer.DeserializeAsync<List<CurrencyResponseDTO>>(stream, options, ct);

                if(deserializedResult != null)
                {
                    lstResult = deserializedResult;
                }
            }
            else
            {
                logger.LogError("Error al recuperar la información del servicio Frankfurter");
            }

            return lstResult;

        }

        public async Task<List<CurrencyResponseDTO>> GetCurrenciesByCodeAsync(string code, CancellationToken ct)
        {
            List<CurrencyResponseDTO> lstResult = new List<CurrencyResponseDTO>();
            var shortcutUrl = $"{rootPath}/currencies/{code}";
            var httpClientShort = clientFactory.CreateClient("FrankfurterClient");
            HttpResponseMessage response = await httpClientShort.GetAsync(shortcutUrl, ct);

            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync(ct);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var deserializedResult = await JsonSerializer.DeserializeAsync<List<CurrencyResponseDTO>>(stream, options, ct);

                if (deserializedResult != null)
                {
                    lstResult = deserializedResult;
                }
            }
            else
            {
                logger.LogError("Error al recuperar la información del servicio Frankfurter");
            }

            return lstResult;
        }

        public async Task<List<CurrencyResponseDTO>> GetLegacyCurrenciesAsync(CancellationToken ct)
        {
            List<CurrencyResponseDTO> lstResult = new List<CurrencyResponseDTO>();
            var shortcutUrl = $"{rootPath}/currencies?scope=all";
            var httpClientShort = clientFactory.CreateClient("FrankfurterClient");
            HttpResponseMessage response = await httpClientShort.GetAsync(shortcutUrl, ct);

            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync(ct);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var deserializedResult = await JsonSerializer.DeserializeAsync<List<CurrencyResponseDTO>>(stream, options, ct);

                if (deserializedResult != null)
                {
                    lstResult = deserializedResult;
                }
            }
            else
            {
                logger.LogError("Error al recuperar la información del servicio Frankfurter");
            }

            return lstResult;
        }        

        #endregion

        #region Providers

        public async Task<ProviderResponseDTO> GetProviderDetailAsync(string providerKey, CancellationToken ct)
        {
            ProviderResponseDTO oProvider = null!;
            var shortcutUrl = $"{rootPath}/providers/{providerKey}";
            var httpClientShort = clientFactory.CreateClient("FrankfurterClient");
            HttpResponseMessage response = await httpClientShort.GetAsync(shortcutUrl, ct);

            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync(ct);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var deserializedResult = await JsonSerializer.DeserializeAsync<ProviderResponseDTO>(stream, options, ct);

                if (deserializedResult != null)
                {
                    oProvider = deserializedResult;
                }
            }
            else
            {
                logger.LogError("Error al recuperar la información del servicio Frankfurter");
            }

            return oProvider!;
        }

        public async Task<List<ProviderResponseDTO>> GetProvidersAsync(CancellationToken ct)
        {
            List<ProviderResponseDTO> lstResult = new List<ProviderResponseDTO>();
            var shortcutUrl = $"{rootPath}/providers";
            var httpClientShort = clientFactory.CreateClient("FrankfurterClient");
            HttpResponseMessage response = await httpClientShort.GetAsync(shortcutUrl, ct);

            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync(ct);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var deserializedResult = await JsonSerializer.DeserializeAsync<List<ProviderResponseDTO>>(stream, options, ct);

                if (deserializedResult != null)
                {
                    lstResult = deserializedResult;
                }
            }
            else
            {
                logger.LogError("Error al recuperar la información del servicio Frankfurter");
            }

            return lstResult;
        }

        #endregion

        #region Rates

        public async Task<List<RateResponseDTO>> GetLatestRatesAsync(CancellationToken ct)
        {
            List<RateResponseDTO> lstResult = new List<RateResponseDTO>();
            var shortcutUrl = $"{rootPath}/rates";
            var httpClientShort = clientFactory.CreateClient("FrankfurterClient");
            HttpResponseMessage response = await httpClientShort.GetAsync(shortcutUrl, ct);

            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync(ct);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var deserializedResult = await JsonSerializer.DeserializeAsync<List<RateResponseDTO>>(stream, options, ct);

                if (deserializedResult != null)
                {
                    lstResult = deserializedResult;
                }
            }
            else
            {
                logger.LogError("Error al recuperar la información del servicio Frankfurter");
            }

            return lstResult;
        }

        public async Task<List<RateResponseDTO>> GetRatesByBaseAsync(string baseRate, CancellationToken ct)
        {
            List<RateResponseDTO> lstResult = new List<RateResponseDTO>();
            var shortcutUrl = $"{rootPath}/rates?base={baseRate}";
            var httpClientShort = clientFactory.CreateClient("FrankfurterClient");
            HttpResponseMessage response = await httpClientShort.GetAsync(shortcutUrl, ct);

            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync(ct);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var deserializedResult = await JsonSerializer.DeserializeAsync<List<RateResponseDTO>>(stream, options, ct);

                if (deserializedResult != null)
                {
                    lstResult = deserializedResult;
                }
            }
            else
            {
                logger.LogError("Error al recuperar la información del servicio Frankfurter");
            }

            return lstResult;
        }

        public async Task<List<RateResponseDTO>> GetRatesByDateAsync(DateTime date, CancellationToken ct)
        {
            List<RateResponseDTO> lstResult = new List<RateResponseDTO>();
            var shortcutUrl = $"{rootPath}/rates?date={date}";
            var httpClientShort = clientFactory.CreateClient("FrankfurterClient");
            HttpResponseMessage response = await httpClientShort.GetAsync(shortcutUrl, ct);

            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync(ct);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var deserializedResult = await JsonSerializer.DeserializeAsync<List<RateResponseDTO>>(stream, options, ct);

                if (deserializedResult != null)
                {
                    lstResult = deserializedResult;
                }
            }
            else
            {
                logger.LogError("Error al recuperar la información del servicio Frankfurter");
            }

            return lstResult;
        }

        public async Task<List<RateResponseDTO>> GetRatesByPeriodAndQuoteAsync(DateTime date, string quote, CancellationToken ct)
        {
            List<RateResponseDTO> lstResult = new List<RateResponseDTO>();
            var shortcutUrl = $"{rootPath}/rates?from={date}&quotes={quote}";
            var httpClientShort = clientFactory.CreateClient("FrankfurterClient");
            HttpResponseMessage response = await httpClientShort.GetAsync(shortcutUrl, ct);

            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync(ct);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var deserializedResult = await JsonSerializer.DeserializeAsync<List<RateResponseDTO>>(stream, options, ct);

                if (deserializedResult != null)
                {
                    lstResult = deserializedResult;
                }
            }
            else
            {
                logger.LogError("Error al recuperar la información del servicio Frankfurter");
            }

            return lstResult;
        }

        public async Task<List<RateResponseDTO>> GetRatesByProviderAsync(string provider, CancellationToken ct)
        {
            List<RateResponseDTO> lstResult = new List<RateResponseDTO>();
            var shortcutUrl = $"{rootPath}/rates";
            var httpClientShort = clientFactory.CreateClient("FrankfurterClient");
            HttpResponseMessage response = await httpClientShort.GetAsync(shortcutUrl, ct);

            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync(ct);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var deserializedResult = await JsonSerializer.DeserializeAsync<List<RateResponseDTO>>(stream, options, ct);

                if (deserializedResult != null)
                {
                    lstResult = deserializedResult;
                }
            }
            else
            {
                logger.LogError("Error al recuperar la información del servicio Frankfurter");
            }

            return lstResult;
        }

        public async Task<List<RateResponseDTO>> GetRatesByProvidersAsync(CancellationToken ct)
        {
            List<RateResponseDTO> lstResult = new List<RateResponseDTO>();
            var shortcutUrl = $"{rootPath}/rates?expand=providers";
            var httpClientShort = clientFactory.CreateClient("FrankfurterClient");
            HttpResponseMessage response = await httpClientShort.GetAsync(shortcutUrl, ct);

            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync(ct);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var deserializedResult = await JsonSerializer.DeserializeAsync<List<RateResponseDTO>>(stream, options, ct);

                if (deserializedResult != null)
                {
                    lstResult = deserializedResult;
                }
            }
            else
            {
                logger.LogError("Error al recuperar la información del servicio Frankfurter");
            }

            return lstResult;
        }

        public async Task<List<RateResponseDTO>> GetRatesByQuotesAsync(IEnumerable<string> quotes, CancellationToken ct)
        {
            List<RateResponseDTO> lstResult = new List<RateResponseDTO>();

            string qData = string.Join(",", quotes);
            var shortcutUrl = $"{rootPath}/rates?quotes={qData}";
            var httpClientShort = clientFactory.CreateClient("FrankfurterClient");
            HttpResponseMessage response = await httpClientShort.GetAsync(shortcutUrl, ct);

            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync(ct);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var deserializedResult = await JsonSerializer.DeserializeAsync<List<RateResponseDTO>>(stream, options, ct);

                if (deserializedResult != null)
                {
                    lstResult = deserializedResult;
                }
            }
            else
            {
                logger.LogError("Error al recuperar la información del servicio Frankfurter");
            }

            return lstResult;
        }

        public async Task<List<RateResponseDTO>> GetRatesGroupByPeriodAsync(DateTime date, string typeGroyp, CancellationToken ct)
        {
            List<RateResponseDTO> lstResult = new List<RateResponseDTO>();
            var shortcutUrl = $"{rootPath}/rates?from={date}&group={typeGroyp}";
            var httpClientShort = clientFactory.CreateClient("FrankfurterClient");
            HttpResponseMessage response = await httpClientShort.GetAsync(shortcutUrl, ct);

            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync(ct);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var deserializedResult = await JsonSerializer.DeserializeAsync<List<RateResponseDTO>>(stream, options, ct);

                if (deserializedResult != null)
                {
                    lstResult = deserializedResult;
                }
            }
            else
            {
                logger.LogError("Error al recuperar la información del servicio Frankfurter");
            }

            return lstResult;
        }

        public async Task<RateResponseDTO> GetRateByPairCurrenciesDateAsync(string baseCurrency, string quoteCurrency, string date, CancellationToken ct)
        {
            RateResponseDTO oCurrency = null!;
            var shortcutUrl = $"{rootPath}/rate/{baseCurrency}/{quoteCurrency}?date={date}";
            var httpClientShort = clientFactory.CreateClient("FrankfurterClient");
            HttpResponseMessage response = await httpClientShort.GetAsync(shortcutUrl, ct);

            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync(ct);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var deserializedResult = await JsonSerializer.DeserializeAsync<RateResponseDTO>(stream, options, ct);

                if (deserializedResult != null)
                {
                    oCurrency = deserializedResult;
                }
            }
            else
            {
                logger.LogError("Error al recuperar la información del servicio Frankfurter");
            }

            return oCurrency!;
        }
        #endregion
    }
}
