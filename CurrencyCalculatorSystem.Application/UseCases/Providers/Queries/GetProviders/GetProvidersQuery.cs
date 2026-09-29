using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Application.DTO.Response.Providers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.UseCases.Providers.Queries.GetProviders
{
    public record GetProvidersQuery : IRequest<OperationResult<GetProvidersResponseDTO>>;
}
