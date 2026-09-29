using CurrencyCalculatorSystem.Application.Contracts.ContextApplication;
using CurrencyCalculatorSystem.Application.DTO.Request.Rates;
using CurrencyCalculatorSystem.Application.UseCases.Currencies.Queries.GetCurrencies;
using CurrencyCalculatorSystem.Application.UseCases.Rates.Queries.GetRateByPairCurrencies;
using CurrencyCalculatorSystem.Application.UseCases.Rates.Queries.GetRatesByPeriodAndQuote;
using CurrencyCalculatorSystem.Presentation.Localization.Filters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyCalculatorSystem.Presentation.Controllers
{
    [TypeFilter(typeof(LogActionFilterAttribute))]
    public class RateController : Controller
    {
        private readonly ILogger<RateController> _logger;
        protected readonly IMediator _mediator;
        protected readonly ILocalizer _localizer;

        public RateController(
            ILogger<RateController> logger,
            IMediator mediator,
            ILocalizer localizer)
        {
            _logger = logger;
            _mediator = mediator;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> GetRateBaseQuoteDate(GetRateByPairCurrenciesRequestDTO request)
        {
            try
            {
                var query = new GetRateByPairCurrenciesQuery(request);
                var result = await _mediator.Send(query);
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error get rate");
                return StatusCode(500, new { message = _localizer.GetMessage($"Error al recuperar el Rate: {ex.Message}") });
            }
        }
    }
}
