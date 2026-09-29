using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.Response.Currencies;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.UseCases.Currencies.Queries.GetCurrenciesByCode
{
    public record GetCurrenciesByCodeQuery : IRequest<OperationResult<GetCurrenciesByCodeResponseDTO>>;
}
