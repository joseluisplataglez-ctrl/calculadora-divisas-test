using CurrencyCalculatorSystem.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Infrastructure.Services
{
    public abstract class BaseExternalService(HttpClient httpClient)
    {
        protected readonly HttpClient HttpClient = httpClient;

        protected static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = false
        };

        // --- Public / Protected Methos for child classes

        protected Task<TResponse> GetAsync<TResponse>(string endpoint, CancellationToken ct)
            => SendAsync<TResponse>(() => HttpClient.GetAsync(endpoint, ct), endpoint, ct);

        private async Task<TResponse> SendAsync<TResponse>(Func<Task<HttpResponseMessage>> send, string endpoint, CancellationToken ct)
        {
            var host = HttpClient.BaseAddress?.Host ?? "Unknow";

            try
            {
                var response = await send();
                return await ProcessResponseAsync<TResponse>(response, endpoint, ct);
            }
            catch(HttpRequestException ex)
            {
                throw new ExternalServiceException(host, endpoint, 500, "HTTP request failed: " + ex.Message);
            }
        }

        protected virtual async Task<TResponse?> ProcessResponseAsync<TResponse>(
            HttpResponseMessage response, string endpoint, CancellationToken ct)
        {
            if (response.IsSuccessStatusCode)
            {
                if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default;
                }

                return await response.Content.ReadFromJsonAsync<TResponse>(JsonOptions, ct);
            }

            // Error Handling

            var body = await response.Content.ReadAsStringAsync(ct);
            var host = HttpClient.BaseAddress?.Host ?? "unknow";

            throw new ExternalServiceException(
                host,
                endpoint,
                (int)response.StatusCode,
                body);
        }
    }
}
