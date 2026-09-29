using AutoMapper;
using CurrencyCalculatorSystem.Application.Contracts.Data;
using CurrencyCalculatorSystem.Application.Contracts.Services;
using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.Response.Providers;
using CurrencyCalculatorSystem.Application.Exceptions;
using CurrencyCalculatorSystem.Application.UseCases.Providers.Queries.GetProviderDetail;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.UseCases.Providers.Queries.GetProviders
{
    public class GetProvidersQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFrankfurter frankfurterServvice,
        ILogger<GetProvidersQueryHandler> logger,
        IOptions<ExternalServiceConfiguration> settings
        ) : IRequestHandler<GetProvidersQuery, OperationResult<GetProvidersResponseDTO>>
    {
        public async Task<OperationResult<GetProvidersResponseDTO>> Handle(GetProvidersQuery request, CancellationToken cancellationToken)
        {
            var response = await frankfurterServvice.GetProvidersAsync(cancellationToken);

            if (response != null)
                throw new ExternalServiceException("FrankfurterService", "GetProvidersAsync", 500);

            return OperationResult.With(mapper.Map<GetProvidersResponseDTO>(response));
        }
    }
}
