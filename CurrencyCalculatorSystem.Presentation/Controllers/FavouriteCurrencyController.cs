using CurrencyCalculatorSystem.Application.Contracts.ContextApplication;
using CurrencyCalculatorSystem.Application.DTO.Request.FavouriteCurrency;
using CurrencyCalculatorSystem.Application.UseCases.FavouriteCurrencies.Commands.CreateFavouroteCurrency;
using CurrencyCalculatorSystem.Application.UseCases.FavouriteCurrencies.Commands.DeleteFavouriteCurrency;
using CurrencyCalculatorSystem.Application.UseCases.FavouriteCurrencies.Queries.GetFavouritesCurrencies;
using CurrencyCalculatorSystem.Presentation.Localization.Filters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyCalculatorSystem.Presentation.Controllers
{
    [TypeFilter(typeof(LogActionFilterAttribute))]
    public class FavouriteCurrencyController : Controller
    {
        private readonly ILogger<FavouriteCurrencyController> _logger;
        protected readonly IMediator _mediator;
        protected readonly ILocalizer _localizer;

        public FavouriteCurrencyController(
            ILogger<FavouriteCurrencyController> logger,
            IMediator mediator,
            ILocalizer localizer)
        {
            _logger = logger;
            _mediator = mediator;
            _localizer = localizer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFavouriteCurrency(CreateFavouriteCurrencyRequestDTO request)
        {
            var command = new CreateFavouroteCurrencyCommand(request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetFavouriteCurrencies(GetFavouriteCurrenciesRequestDTO request)
        {
            var query = new GetFavouritesCurrenciesQuery(request);
            var result = await _mediator.Send(query);
            return Json(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFavouriteCurrency(DeleteFavouriteCurrencyRequestDTO request)
        {
            var query = new DeleteFavouriteCurrencyCommand(request);
            var result = await _mediator.Send(query);
            return Json(result);
        }
    }
}
