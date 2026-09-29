using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.UseCases.FavouriteCurrencies.Commands.DeleteFavouriteCurrency
{
    public class DeleteFavouriteCurrencyCommandValidator : AbstractValidator<DeleteFavouriteCurrencyCommand>
    {
        public DeleteFavouriteCurrencyCommandValidator()
        {
            RuleFor(x => x.request.FavouriteCurrency)
                .NotEmpty()
                .WithMessage("El nombre de la divisa es requerido");
        }
    }
}
