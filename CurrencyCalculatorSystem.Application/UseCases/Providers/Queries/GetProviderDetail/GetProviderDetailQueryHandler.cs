using AutoMapper;
using CurrencyCalculatorSystem.Application.Contracts.Data;
using CurrencyCalculatorSystem.Application.Contracts.Services;
using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.Response.Providers;
using CurrencyCalculatorSystem.Application.DTO.Response.Rates;
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

namespace CurrencyCalculatorSystem.Application.UseCases.Providers.Queries.GetProviderDetail
{
    public class GetProviderDetailQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFrankfurter frankfurterServvice,
        ILogger<GetProviderDetailQueryHandler> logger,
        IOptions<ExternalServiceConfiguration> settings
        ) : IRequestHandler<GetProviderDetailQuery, OperationResult<GetProviderDetailResponseDTO>>
    {
        public async Task<OperationResult<GetProviderDetailResponseDTO>> Handle(GetProviderDetailQuery request, CancellationToken cancellationToken)
        {
            var response = await frankfurterServvice.GetProviderDetailAsync("Test", cancellationToken);

            if (response != null)
                throw new ExternalServiceException("FrankfurterService", "GetProviderDetailAsync", 500);

            return OperationResult.With(mapper.Map<GetProviderDetailResponseDTO>(response));
        }
    }
}
