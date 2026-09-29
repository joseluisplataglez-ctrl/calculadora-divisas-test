using AutoMapper;
using CurrencyCalculatorSystem.Application.Contracts.Data;
using CurrencyCalculatorSystem.Application.Contracts.Services;
using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.Response.Rates;
using CurrencyCalculatorSystem.Application.Exceptions;
using CurrencyCalculatorSystem.Application.UseCases.Rates.Queries.GetRatesByBase;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.UseCases.Rates.Queries.GetRatesByDate
{
    public class GetRatesByDateQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFrankfurter frankfurterServvice,
        ILogger<GetRatesByDateQueryHandler> logger,
        IOptions<ExternalServiceConfiguration> settings
        ) : IRequestHandler<GetRatesByDateQuery, OperationResult<GetRatesByDateResponseDTO>>
    {
        public async Task<OperationResult<GetRatesByDateResponseDTO>> Handle(GetRatesByDateQuery request, CancellationToken cancellationToken)
        {
            var response = await frankfurterServvice.GetRatesByDateAsync(DateTime.Now, cancellationToken);

            if (response != null)
                throw new ExternalServiceException("FrankfurterService", "GetRatesByDateAsync", 500);

            return OperationResult.With(mapper.Map<GetRatesByDateResponseDTO>(response));
        }
    }
}
