using CurrencyCalculatorSystem.Application.Contracts.ContextApplication;
using CurrencyCalculatorSystem.Application.Contracts.Data;
using CurrencyCalculatorSystem.Application.Contracts.Services;
using CurrencyCalculatorSystem.Application.DTO.Common;
using CurrencyCalculatorSystem.Infrastructure.Data;
using CurrencyCalculatorSystem.Infrastructure.Data.Repositories;
using CurrencyCalculatorSystem.Infrastructure.Services.Frankfurter;
using CurrencyCalculatorSystem.Infrastructure.Services.Localization;
using CurrencyCalculatorSystem.Infrastructure.Services.LogActivity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculatorSystem.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContextDb>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ContextDb")!,
                x => x.MigrationsHistoryTable("__ef_migrations_history", "infra")));

            services.AddDbContext<AuditDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ContextDb")));

            // Repositorios, UnitOfWork
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(ICatalogRepository<>), typeof(CatalogRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISaveLog, ActivityLog>();
            services.AddSingleton(typeof(ILocalizer), typeof(Localizer));

            // Servicios externos            
            services.AddHttpClient();
            services.AddScoped<IFrankfurter, FrankfurterServices>();

            return services;

        }
    }
}
