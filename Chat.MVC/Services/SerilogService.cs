using Serilog.Events;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Chat.MVC.Services;

public class SerilogService
{
    public static void SerilogSettings(IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
           .ReadFrom.Configuration(configuration)
           .WriteTo.Console(theme: AnsiConsoleTheme.Code)
           .MinimumLevel.Information()
           .Enrich.FromLogContext()
           .Enrich.WithClientIp()
           .WriteTo.Telegram(configuration.GetConnectionString("TelegramToken"), "33780774")
           .CreateLogger();
           
           
          
    }
}
