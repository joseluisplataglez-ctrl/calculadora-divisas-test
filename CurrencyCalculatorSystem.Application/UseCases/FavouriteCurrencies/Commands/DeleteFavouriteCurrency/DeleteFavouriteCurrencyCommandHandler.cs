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

namespace CurrencyCalculatorSystem.Application.UseCases.FavouriteCurrencies.Commands.DeleteFavouriteCurrency
{
    public class DeleteFavouriteCurrencyCommandHandler(
        IBaseRepository<Domain.Aggregates.FavouriteCurrencies> repository,
        IMapper mapper,
        ILogger<CreateFavouroteCurrencyCommandHandler> logger
        )
        : IRequestHandler<DeleteFavouriteCurrencyCommand, OperationResult<bool>>
    {
        public async Task<OperationResult<bool>> Handle(DeleteFavouriteCurrencyCommand request, CancellationToken cancellationToken)
        {
            var fc = await repository.GetListAsync(x => x.FavouriteCurrency == request.request.FavouriteCurrency);
            repository.Delete(fc.FirstOrDefault()!);
            return OperationResult.With(true);
        }
    }
}
