using Serilog.Events;
using Serilog;

namespace Chat.MVC.Services;

public class SerilogService
{
    public static void SerilogSettings(IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
           .ReadFrom.Configuration(configuration)
           .WriteTo.Console()
           .MinimumLevel.Information()
           .Enrich.FromLogContext()
           .Enrich.WithClientIp()
           .WriteTo.Telegram(configuration.GetConnectionString("TelegramToken"), "33780774")
           .MinimumLevel.Error()
           .CreateLogger();
           
           //.WriteTo.TeleSink(
           // telegramApiKey: configuration.GetConnectionString("TelegramToken1"),
           // telegramChatId: "619670300",
           // minimumLevel: LogEventLevel.Error)
           //.CreateLogger();
    }
}
