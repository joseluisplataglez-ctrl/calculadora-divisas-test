using CurrencyCalculatorSystem.Application;
using CurrencyCalculatorSystem.Application.Contracts.ContextApplication;
using CurrencyCalculatorSystem.Domain.Common;
using CurrencyCalculatorSystem.Infrastructure;
using CurrencyCalculatorSystem.Infrastructure.Data;
using CurrencyCalculatorSystem.Infrastructure.Services.Localization;
using CurrencyCalculatorSystem.Presentation.ErrorHandling;
using CurrencyCalculatorSystem.Presentation.Middlewares;
using CurrencyCalculatorSystem.Presentation.Profiles;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Globalization;

try
{
    var builder = WebApplication.CreateBuilder(args);

    var connectionString = builder.Configuration.GetConnectionString("ContextDb");

    builder.Services.AddDbContext<ContextDb>(options =>
        options.UseSqlServer(connectionString));

    // Inicialización y configuración de Serilog
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    // Configura el host para usar Serilog como el proveedor de registro de eventos
    builder.Host.UseSerilog();

    // Configuraciones de la internacionalizacion para los mensajes.
    builder.Services.AddLocalization();

    var supportedCultures = new[]
        {
        new CultureInfo("es-MX"),
        new CultureInfo("es-US")
    };

    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        options.DefaultRequestCulture = new RequestCulture("es-MX");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });

    builder.Services.AddSingleton(typeof(ILocalizer), typeof(Localizer));

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    // Registra el middleware global para manejar excepciones no controladas en la aplicación
    builder.Services.AddTransient<IExceptionMapper, DefaultExceptionMapper>();
    // builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
    builder.Services.AddHttpContextAccessor();
    // Registra el servicio para obtener informaciuón sobre el usuario autencticado en la aplicación
    builder.Services.AddScoped<ICurrentUserProvider, HttpCurrentUserProvider>();

    #region Services Registration
    // Registra los servicios de infraestructura, como acceso a datos, repositorios y otros servicios relacionados
    builder.Services.AddInfrastructureServices(builder.Configuration);

    // Registra los servicios de la capa de aplicación, como casos de uso, validaciones y lógica de negocio
    builder.Services.AddApplicationServices();

    builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(PresentationMappingProfile).Assembly));

    //builder.Services.Configure<RootConfiguration>(builder.Configuration);
    #endregion

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var businessContext = services.GetRequiredService<ContextDb>();
            await businessContext.Database.MigrateAsync();

            var auditContext = services.GetRequiredService<AuditDbContext>();
            await auditContext.Database.MigrateAsync();

        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Ocurrió un error al aplicar las migraciones en el inicio de la app.");
        }
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
