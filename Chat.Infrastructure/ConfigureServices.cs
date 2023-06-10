using Chat.Application.Common.Interfaces;
using Chat.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Chat.Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            options.UseLazyLoadingProxies();
            options.UseNpgsql(configuration.GetConnectionString("DbConnection"));
        });

        return services;
    }
}
