using AutoMapper;
using CurrencyCalculatorSystem.Application.Contracts.Data;
using CurrencyCalculatorSystem.Application.Contracts.Services;
using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.Response.Rates;
using CurrencyCalculatorSystem.Application.Exceptions;
using CurrencyCalculatorSystem.Application.UseCases.Rates.Queries.GetRatesByDate;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.UseCases.Rates.Queries.GetRatesByPeriodAndQuote
{
    public class GetRatesByPeriodAndQuoteQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFrankfurter frankfurterServvice,
        ILogger<GetRatesByPeriodAndQuoteQueryHandler> logger,
        IOptions<ExternalServiceConfiguration> settings
        ) : IRequestHandler<GetRatesByPeriodAndQuoteQuery, OperationResult<GetRatesByPeriodAndQuoteResponseDTO>>
    {
        public async Task<OperationResult<GetRatesByPeriodAndQuoteResponseDTO>> Handle(GetRatesByPeriodAndQuoteQuery request, CancellationToken cancellationToken)
        {
            var response = await frankfurterServvice.GetRatesByPeriodAndQuoteAsync(DateTime.Now, "BPM", cancellationToken);

            if (response != null)
                throw new ExternalServiceException("FrankfurterService", "GetRatesByPeriodAndQuoteAsync", 500);

            return OperationResult.With(mapper.Map<GetRatesByPeriodAndQuoteResponseDTO>(response));
        }
    }
}
