using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.Request.Rates;
using CurrencyCalculatorSystem.Application.DTO.Response.Rates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.UseCases.Rates.Queries.GetRateByPairCurrencies
{
    public record GetRateByPairCurrenciesQuery(GetRateByPairCurrenciesRequestDTO request) : IRequest<OperationResult<GetRateByPairCurrenciesResponseDTO>>;
}
