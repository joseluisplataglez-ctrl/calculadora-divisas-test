using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.Request.FavouriteCurrency;
using CurrencyCalculatorSystem.Application.DTO.Response.FavouriteCurrency;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.UseCases.FavouriteCurrencies.Commands.CreateFavouroteCurrency
{
    public record CreateFavouroteCurrencyCommand(CreateFavouriteCurrencyRequestDTO request) : IRequest<OperationResult<CreateFavouriteCurrencyResponseDTO>>;
}
