using AutoMapper;
using CurrencyCalculatorSystem.Application.Contracts.Data;
using CurrencyCalculatorSystem.Application.Contracts.Services;
using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.Request.Currencies;
using CurrencyCalculatorSystem.Application.DTO.Response.Currencies;
using CurrencyCalculatorSystem.Application.Exceptions;
using CurrencyCalculatorSystem.Application.UseCases.Currencies.Queries.GetCurrencies;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.UseCases.Currencies.Queries.GetCurrenciesByCode
{
    public class GetCurrenciesByCodeQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFrankfurter frankfurterServvice,
        ILogger<GetCurrenciesByCodeQueryHandler> logger,
        IOptions<ExternalServiceConfiguration> settings
        ) : IRequestHandler<GetCurrenciesByCodeQuery, OperationResult<GetCurrenciesByCodeResponseDTO>>
    {
        public async Task<OperationResult<GetCurrenciesByCodeResponseDTO>> Handle(GetCurrenciesByCodeQuery request, CancellationToken cancellationToken)
        {
            var response = await frankfurterServvice.GetCurrenciesByCodeAsync("USD",cancellationToken);

            if (response != null)
                throw new ExternalServiceException("FrankfurterService", "GetCurrenciesByCodeAsync", 500);

            return OperationResult.With(mapper.Map<GetCurrenciesByCodeResponseDTO>(response));
        }
    }
}
