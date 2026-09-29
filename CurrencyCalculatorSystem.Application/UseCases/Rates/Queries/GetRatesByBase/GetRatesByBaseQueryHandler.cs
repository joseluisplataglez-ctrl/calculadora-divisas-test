using AutoMapper;
using CurrencyCalculatorSystem.Application.Contracts.Data;
using CurrencyCalculatorSystem.Application.Contracts.Services;
using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.Response.Currencies;
using CurrencyCalculatorSystem.Application.DTO.Response.Rates;
using CurrencyCalculatorSystem.Application.Exceptions;
using CurrencyCalculatorSystem.Application.UseCases.Rates.Queries.GetRateByPairCurrencies;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.UseCases.Rates.Queries.GetRatesByBase
{
    public class GetRatesByBaseQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFrankfurter frankfurterServvice,
        ILogger<GetRatesByBaseQueryHandler> logger,
        IOptions<ExternalServiceConfiguration> settings
        ) : IRequestHandler<GetRatesByBaseQuery, OperationResult<GetRatesByBaseResponseDTO>>
    {
        public async Task<OperationResult<GetRatesByBaseResponseDTO>> Handle(GetRatesByBaseQuery request, CancellationToken cancellationToken)
        {
            var response = await frankfurterServvice.GetRatesByBaseAsync("Test",cancellationToken);

            if (response != null)
                throw new ExternalServiceException("FrankfurterService", "GetRatesByBase", 500);

            return OperationResult.With(mapper.Map<GetRatesByBaseResponseDTO>(response));
        }
    }
}
