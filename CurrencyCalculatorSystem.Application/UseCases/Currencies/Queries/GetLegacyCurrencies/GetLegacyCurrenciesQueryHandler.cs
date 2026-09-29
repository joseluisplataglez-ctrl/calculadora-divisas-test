using AutoMapper;
using CurrencyCalculatorSystem.Application.Contracts.Data;
using CurrencyCalculatorSystem.Application.Contracts.Services;
using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.Response.Currencies;
using CurrencyCalculatorSystem.Application.Exceptions;
using CurrencyCalculatorSystem.Application.UseCases.Currencies.Queries.GetCurrenciesByCode;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.UseCases.Currencies.Queries.GetLegacyCurrencies
{
    public class GetLegacyCurrenciesQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFrankfurter frankfurterServvice,
        ILogger<GetLegacyCurrenciesQueryHandler> logger,
        IOptions<ExternalServiceConfiguration> settings
        ) : IRequestHandler<GetLegacyCurrenciesQuery, OperationResult<GetLegacyCurrenciesResponseDTO>>
    {
        public async Task<OperationResult<GetLegacyCurrenciesResponseDTO>> Handle(GetLegacyCurrenciesQuery request, CancellationToken cancellationToken)
        {
            var response = await frankfurterServvice.GetLegacyCurrenciesAsync(cancellationToken);

            if (response != null)
                throw new ExternalServiceException("FrankfurterService", "GetLegacyCurrenciesAsync", 500);

            return OperationResult.With(mapper.Map<GetLegacyCurrenciesResponseDTO>(response));
        }
    }
}
