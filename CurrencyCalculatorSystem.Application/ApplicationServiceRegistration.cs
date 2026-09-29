using CurrencyCalculatorSystem.Application.Behaviors;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using FluentValidation;

namespace CurrencyCalculatorSystem.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ApplicationServiceRegistration).Assembly));
            services.AddValidatorsFromAssembly(typeof(ApplicationServiceRegistration).Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ApplicationServiceRegistration).Assembly));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddLocalization();

            var supportedCultures = new[]
        {
            new CultureInfo("es-MX"),
            new CultureInfo("en-US")
        };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("es-MX");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            return services;
        }
    }
}
