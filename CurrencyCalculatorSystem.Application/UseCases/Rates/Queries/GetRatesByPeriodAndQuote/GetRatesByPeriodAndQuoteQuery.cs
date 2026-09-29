using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.Response.Rates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.UseCases.Rates.Queries.GetRatesByPeriodAndQuote
{
    public record GetRatesByPeriodAndQuoteQuery : IRequest<OperationResult<GetRatesByPeriodAndQuoteResponseDTO>>;
}
