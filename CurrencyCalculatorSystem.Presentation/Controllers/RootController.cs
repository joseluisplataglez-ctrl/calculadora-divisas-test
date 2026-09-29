using CurrencyCalculatorSystem.Application.Contracts.ContextApplication;
using CurrencyCalculatorSystem.Presentation.Localization.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyCalculatorSystem.Presentation.Controllers
{
    [Produces("application/json")]
    [TypeFilter(typeof(LogActionFilterAttribute))]
    public class BaseController : Controller
    {
        protected readonly IMediator _mediator;
        protected readonly ILocalizer _localizer;

        protected BaseController(IMediator mediator, ILocalizer localizer)
        {
            _mediator = mediator;
            _localizer = localizer;
        }
    }
}
