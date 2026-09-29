using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.Request.FavouriteCurrency;
using CurrencyCalculatorSystem.Application.DTO.Response.FavouriteCurrency;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.UseCases.FavouriteCurrencies.Commands.DeleteFavouriteCurrency
{
    public record DeleteFavouriteCurrencyCommand(DeleteFavouriteCurrencyRequestDTO request) : IRequest<OperationResult<bool>>;
}
