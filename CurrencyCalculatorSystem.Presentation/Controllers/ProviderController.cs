using CurrencyCalculatorSystem.Application.Contracts.ContextApplication;
using CurrencyCalculatorSystem.Presentation.Localization.Filters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyCalculatorSystem.Presentation.Controllers
{
    [TypeFilter(typeof(LogActionFilterAttribute))]
    public class ProviderController : Controller
    {
        private readonly ILogger<ProviderController> _logger;
        protected readonly IMediator _mediator;
        protected readonly ILocalizer _localizer;

        public ProviderController(
            ILogger<ProviderController> logger,
            IMediator mediator,
            ILocalizer localizer)
        {
            _logger = logger;
            _mediator = mediator;
            _localizer = localizer;
        }
    }
}
