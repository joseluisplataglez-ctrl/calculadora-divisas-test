using AutoMapper;
using CurrencyCalculatorSystem.Application.Contracts.Data;
using CurrencyCalculatorSystem.Application.Contracts.Services;
using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.Request.FavouriteCurrency;
using CurrencyCalculatorSystem.Application.DTO.Response.Rates;
using CurrencyCalculatorSystem.Application.Exceptions;
using CurrencyCalculatorSystem.Application.UseCases.FavouriteCurrencies.Commands.CreateFavouroteCurrency;
using CurrencyCalculatorSystem.Application.UseCases.Providers.Queries.GetProviders;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.UseCases.Rates.Queries.GetRateByPairCurrencies
{
    public class GetRateByPairCurrenciesQueryHandler(
        IUnitOfWork unitOfWork,
        IMediator mediator,
        IMapper mapper,
        IFrankfurter frankfurterServvice,
        ILogger<GetRateByPairCurrenciesQueryHandler> logger,
        IOptions<ExternalServiceConfiguration> settings
        ) : IRequestHandler<GetRateByPairCurrenciesQuery, OperationResult<GetRateByPairCurrenciesResponseDTO>>
    {
        public async Task<OperationResult<GetRateByPairCurrenciesResponseDTO>> Handle(GetRateByPairCurrenciesQuery request, CancellationToken cancellationToken)
        {            
            var response = await frankfurterServvice.GetRateByPairCurrenciesDateAsync(
                request.request.baseCurrency, request.request.quoteCurrency,request.request.date,cancellationToken);

            if (response == null)
                throw new ExternalServiceException("FrankfurterService", "GetRateByPairCurrenciesAsync", 500);

            var commandFavourite = new CreateFavouroteCurrencyCommand(new CreateFavouriteCurrencyRequestDTO { FavouriteCurrencyReq = request.request.quoteCurrency});
            var result = await mediator.Send(commandFavourite);

            await unitOfWork.CompleteAsync();

            return OperationResult.With(mapper.Map<GetRateByPairCurrenciesResponseDTO>(response));
        }
    }
}
