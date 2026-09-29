using AutoMapper;
using CurrencyCalculatorSystem.Application.Contracts.Data;
using CurrencyCalculatorSystem.Application.Contracts.Services;
using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.Response.Providers;
using CurrencyCalculatorSystem.Application.DTO.Response.Rates;
using CurrencyCalculatorSystem.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.UseCases.Rates.Queries.GetLatestRates
{
    public class GetLatestRatesQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFrankfurter frankfurterServvice,
        ILogger<GetLatestRatesQueryHandler> logger,
        IOptions<ExternalServiceConfiguration> settings
        ) : IRequestHandler<GetLatestRatesQuery, OperationResult<GetLatestRatesResponseDTO>>
    {
        public async Task<OperationResult<GetLatestRatesResponseDTO>> Handle(GetLatestRatesQuery request, CancellationToken cancellationToken)
        {
            var response = await frankfurterServvice.GetLatestRatesAsync(cancellationToken);

            if (response != null)
                throw new ExternalServiceException("FrankfurterService", "GetLatestRatesAsync", 500);

            return OperationResult.With(mapper.Map<GetLatestRatesResponseDTO>(response));
        }
    }
}
