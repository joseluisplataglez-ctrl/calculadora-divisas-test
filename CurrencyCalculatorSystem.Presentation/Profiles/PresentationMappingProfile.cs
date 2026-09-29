using AutoMapper;
using CurrencyCalculatorSystem.Application.DTO.Response.Currencies;
using CurrencyCalculatorSystem.Application.DTO.Response.Providers;
using CurrencyCalculatorSystem.Application.DTO.Response.Rates;
using CurrencyCalculatorSystem.Presentation.ViewModels.Currencies;
using CurrencyCalculatorSystem.Presentation.ViewModels.Providers;
using CurrencyCalculatorSystem.Presentation.ViewModels.Rates;

namespace CurrencyCalculatorSystem.Presentation.Profiles
{
    public class PresentationMappingProfile : Profile
    {
        public PresentationMappingProfile()
        {
            CreateMap<GetLatestRatesResponseDTO, RateViewModel>();
            CreateMap<GetCurrenciesResponseDTO, CurrencyViewModel>();
            CreateMap<GetProvidersResponseDTO, ProviderViewModel>();
        }
    }
}
