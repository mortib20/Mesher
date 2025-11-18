using Mesher.Database;
using Mesher.Mesh;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

namespace Mesher.Main;

public abstract class Program
{
    public static async Task Main(string[] args)
    {
        const string logFormat = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3}{Scope}] {Message:lj}{NewLine}{Exception}";
        var logPath = Path.Combine(Environment.CurrentDirectory, "logs");
        if (!Directory.Exists(logPath))
        {
            Directory.CreateDirectory(logPath);
        }

        Log.Logger = new LoggerConfiguration()
            .Enrich.WithDemystifiedStackTraces()
            .WriteTo.Console(LogEventLevel.Information, logFormat)
            .WriteTo.File(
                Path.Combine(logPath, "aviator.txt"),
                restrictedToMinimumLevel: LogEventLevel.Verbose,
                rollingInterval: RollingInterval.Month,
                outputTemplate: logFormat
            )
            .CreateLogger();
        
        var builder = WebApplication.CreateBuilder(args);
        
        // Logging
        builder.Services.AddSerilog();

        // Database
        builder.Services.AddDbContextPool<MesherContext>(opt =>
        {
            opt.UseNpgsql(builder.Configuration.GetConnectionString("default"), o =>
            {
                o.UseNodaTime();
            });
            opt.EnableDetailedErrors();
            opt.EnableSensitiveDataLogging();
        });

        builder.Services.AddHostedService<MeshService>();
        
        var app = builder.Build();
        
        // Database
        await using (var scope = app.Services.CreateAsyncScope())
        {
            var services = scope.ServiceProvider;

            var logger = services.GetRequiredService<ILogger<Program>>();
            
            logger.LogInformation("Applying migrations");
            
            var context = services.GetRequiredService<MesherContext>();

            if (app.Environment.IsDevelopment())
            {
                await context.Database.EnsureCreatedAsync();
            }
            else
            {
                await context.Database.MigrateAsync();
            }
        }

        await app.RunAsync();
    }
}