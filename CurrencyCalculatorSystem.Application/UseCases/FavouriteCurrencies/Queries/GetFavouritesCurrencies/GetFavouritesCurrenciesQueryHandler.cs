using AutoMapper;
using CurrencyCalculatorSystem.Application.Contracts.Data;
using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.Response.FavouriteCurrency;
using CurrencyCalculatorSystem.Application.UseCases.FavouriteCurrencies.Commands.CreateFavouroteCurrency;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.UseCases.FavouriteCurrencies.Queries.GetFavouritesCurrencies
{
    public class GetFavouritesCurrenciesQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<CreateFavouroteCurrencyCommandHandler> logger
        ) : IRequestHandler<GetFavouritesCurrenciesQuery, OperationResult<List<GetFavouriteCurrenciesResponseDTO>>>
    {
        public async Task<OperationResult<List<GetFavouriteCurrenciesResponseDTO>>> Handle(GetFavouritesCurrenciesQuery request, CancellationToken cancellationToken)
        {
            var repository = unitOfWork.GetRepository<Domain.Aggregates.FavouriteCurrencies>();

            return OperationResult.With(
                mapper.Map<List<GetFavouriteCurrenciesResponseDTO>>(
                    await repository.GetAllAsync()));
        }
    }
}
