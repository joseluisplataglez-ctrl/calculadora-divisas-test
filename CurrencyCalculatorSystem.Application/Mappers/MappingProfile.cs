using AutoMapper;
using CurrencyCalculatorSystem.Application.DTO.ExternalServices;
using CurrencyCalculatorSystem.Application.DTO.Response.Currencies;
using CurrencyCalculatorSystem.Application.DTO.Response.FavouriteCurrency;
using CurrencyCalculatorSystem.Application.DTO.Response.Rates;
using CurrencyCalculatorSystem.Application.UseCases.Rates.Queries.GetRateByPairCurrencies;
using CurrencyCalculatorSystem.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            EnumMappingProfile();

            CommandToDomainMappingProfile();

            DomainToViewModelMappingProfile();

            ExternalToDTOMappingProfile();
        }

        private void EnumMappingProfile()
        {
            // Mapeo de enumeraciones a DTOs
        }

        private void CommandToDomainMappingProfile()
        {
            // Mapeo de comando a entidades de dominio
            CreateMap<FavouriteCurrencies, CreateFavouriteCurrencyResponseDTO>();
            CreateMap<FavouriteCurrencies, GetFavouriteCurrenciesResponseDTO>();
        }

        private void DomainToViewModelMappingProfile()
        {
            // Mapeo de entidades del dominio a ViewModels
        }
        private void ExternalToDTOMappingProfile()
        {
            // Mapeo de respuestas de servicios externos a DTO
            CreateMap<CurrencyResponseDTO, GetCurrenciesResponseDTO>()
                .ForMember(dest => dest.CurrencyIsoCode,
                       opt => opt.MapFrom(src => src.iso_code))
                .ForMember(dest => dest.CurrencyIsoNumber,
                        opt => opt.MapFrom(src => src.iso_numeric))
                .ForMember(dest => dest.CurrencyName,
                        opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.CurrencySymbol,
                        opt => opt.Ignore());

            CreateMap<RateResponseDTO, GetRateByPairCurrenciesResponseDTO>()
                .ForMember(dest => dest.CurrencyDate, opt => opt.MapFrom(src => DateTime.Parse(src.date)))
                .ForMember(dest => dest.CurrencyBase, opt => opt.MapFrom(src => src.@base))
                .ForMember(dest => dest.QuoteCurrency, opt => opt.MapFrom(src => src.quote))
                .ForMember(dest => dest.RateValue, opt => opt.MapFrom(src => src.rate));

            CreateMap<GetFavouriteCurrenciesResponseDTO, FavouriteCurrencies>()
                .ForMember(dest => dest.FavouriteCurrency, opt => opt.MapFrom(src => src.FavouriteCurrency));

            CreateMap<CreateFavouriteCurrencyResponseDTO, FavouriteCurrencies>();

        }
    }
}
