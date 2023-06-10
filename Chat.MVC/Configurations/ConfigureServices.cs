using Chat.Application.Common.Interfaces;
using Chat.MVC.Services;

namespace Chat.MVC.Configurations;

public static class ConfigureServices
{
    public static IServiceCollection AddChatMVCServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddHttpContextAccessor();
        return services;
    }
}
