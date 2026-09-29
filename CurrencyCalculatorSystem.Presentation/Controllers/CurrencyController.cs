using CurrencyCalculatorSystem.Application.Contracts.ContextApplication;
using CurrencyCalculatorSystem.Application.UseCases.Currencies.Queries.GetCurrencies;
using CurrencyCalculatorSystem.Presentation.Localization.Filters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyCalculatorSystem.Presentation.Controllers
{
    [TypeFilter(typeof(LogActionFilterAttribute))]
    public class CurrencyController : Controller
    {
        private readonly ILogger<CurrencyController> _logger;
        protected readonly IMediator _mediator;
        protected readonly ILocalizer _localizer;

        public CurrencyController(
            ILogger<CurrencyController> logger,
            IMediator mediator,
            ILocalizer localizer)
        {
            _logger = logger;
            _mediator = mediator;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrencies()
        {
            try
            {
                var query = new GetCurrenciesQuery();
                var result = await _mediator.Send(query);
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error converting currency");
                return StatusCode(500, new { message = _localizer.GetMessage($"Error al recuperar las divisas: {ex.Message}") });
            }
        }
    }
}
