using ElektronickePosudky.Api.DTOs;
using ElektronickePosudky.Api.ExceptionHandlers;
using ElektronickePosudky.Api.Filters;
using ElektronickePosudky.Application;
using ElektronickePosudky.Application.Interfaces;
using ElektronickePosudky.Application.Repositories;
using ElektronickePosudky.Infrastructure.Data;
using ElektronickePosudky.Infrastructure.Repositories;
using ElektronickePosudky.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Serilog;

QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

try
{
    WebApplication app;
    RequestLocalizationOptions localizationOptions;

    lock (Program.BootstrapLock)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        Log.Information("Running Elektronicke Posudky API...");
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext());

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<CorrelationIdProblemDetailsFilter>();
        }).ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
        builder.Services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions.Remove("traceId");

                if (context.HttpContext.Request.Headers.TryGetValue("X-Correlation-Id", out var correlationId))
                {
                    if (context.ProblemDetails is ExtendedValidationProblemDetails extended)
                    {
                        if (string.IsNullOrEmpty(extended.CorrelationId))
                        {
                            extended.CorrelationId = correlationId.ToString();
                        }
                    }
                    else if (!context.ProblemDetails.Extensions.ContainsKey("correlationId"))
                    {
                        context.ProblemDetails.Extensions["correlationId"] = correlationId.ToString();
                    }
                }
            };
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddApplication();

        builder.Services.AddLocalization();

        var supportedCultures = new[] { "cs", "en" };
        localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        builder.Services.AddScoped<IPdfExportService, QuestPdfExportService>();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Error: Not found 'DefaultConnection'.");

        Console.WriteLine(connectionString);

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Register repositories
        builder.Services.AddScoped<IPosudekRepository, PosudekRepository>();
        builder.Services.AddScoped<ICiselnikRepository, CiselnikRepository>();

        app = builder.Build();
    }

    app.UseRequestLocalization(localizationOptions);

    app.UseExceptionHandler();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    if (!app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();
    }

    app.MapControllers();

    app.Run();
}
catch (HostAbortedException ex)
{
    Log.Warning(ex, "ignore this error because of EF migration run ");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application crashed (crash) during startup!");
    throw;
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program 
{ 
    public static readonly object BootstrapLock = new();
}