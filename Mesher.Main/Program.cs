using Mesher.Mesh;
using Serilog;
using Serilog.Events;

namespace Mesher.Main;

public class Program
{
    public static void Main(string[] args)
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

        builder.Services.AddLogging();
        
        builder.Services.AddSerilog();


        builder.Services.AddHostedService<MeshService>();
        
        var app = builder.Build();

        

        app.Run();
    }
}