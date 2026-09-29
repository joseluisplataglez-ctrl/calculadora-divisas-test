using AutoMapper;
using CurrencyCalculatorSystem.Application.Contracts.Data;
using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.Response.FavouriteCurrency;
using CurrencyCalculatorSystem.Application.Exceptions;
using CurrencyCalculatorSystem.Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurrencyCalculatorSystem.Application.UseCases.FavouriteCurrencies.Commands.CreateFavouroteCurrency
{
    public class CreateFavouroteCurrencyCommandHandler(
        IBaseRepository<Domain.Aggregates.FavouriteCurrencies> repository,
        IMapper mapper,
        ILogger<CreateFavouroteCurrencyCommandHandler> logger
        ) : IRequestHandler<CreateFavouroteCurrencyCommand, OperationResult<CreateFavouriteCurrencyResponseDTO>>
    {
        public async Task<OperationResult<CreateFavouriteCurrencyResponseDTO>> Handle(CreateFavouroteCurrencyCommand request, CancellationToken cancellationToken)
        {
            var favouriteCurrency = new Domain.Aggregates.FavouriteCurrencies { FavouriteCurrency = request.request.FavouriteCurrencyReq };
            var exist = await repository.GetListAsync(x => x.FavouriteCurrency == request.request.FavouriteCurrencyReq);

            if (exist.Count() > 0)
                return OperationResult.With(mapper.Map<CreateFavouriteCurrencyResponseDTO>(exist.FirstOrDefault()));

            return OperationResult.With(mapper.Map<CreateFavouriteCurrencyResponseDTO>(await repository.AddAsync(favouriteCurrency)));
        }
    }
}
