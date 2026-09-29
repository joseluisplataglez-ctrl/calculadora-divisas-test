using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.UseCases.FavouriteCurrencies.Commands.CreateFavouroteCurrency
{
    public class CreateFavouroteCurrencyCommandValidator : AbstractValidator<CreateFavouroteCurrencyCommand>
    {
        public CreateFavouroteCurrencyCommandValidator()
        {
            RuleFor(x => x.request.FavouriteCurrencyReq)
                .NotEmpty()
                .WithMessage("El Nombre de la divisa es requerida");
        }
    }
}
