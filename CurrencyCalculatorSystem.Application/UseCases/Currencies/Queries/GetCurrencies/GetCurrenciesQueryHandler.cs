using AutoMapper;
using CurrencyCalculatorSystem.Application.Contracts.Data;
using CurrencyCalculatorSystem.Application.Contracts.Services;
using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.ExternalServices;
using CurrencyCalculatorSystem.Application.DTO.Response.Currencies;
using CurrencyCalculatorSystem.Application.Exceptions;
using CurrencyCalculatorSystem.Application.UseCases.Rates.Queries.GetLatestRates;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.UseCases.Currencies.Queries.GetCurrencies
{
    public class GetCurrenciesQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFrankfurter frankfurterServvice,
        ILogger<GetCurrenciesQueryHandler> logger,
        IOptions<ExternalServiceConfiguration> settings        
        ) : IRequestHandler<GetCurrenciesQuery, OperationResult<List<GetCurrenciesResponseDTO>>>
    {
        public async Task<OperationResult<List<GetCurrenciesResponseDTO>>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
        {
            var response = await frankfurterServvice.GetCurrenciesAsync(cancellationToken);

            if (response == null)
                throw new ExternalServiceException("FrankfurterService", "GetCurrenciesAsync", 500);
            var result = mapper.Map<List<GetCurrenciesResponseDTO>>(response);


            return OperationResult.With(result);
        }
    }
}
