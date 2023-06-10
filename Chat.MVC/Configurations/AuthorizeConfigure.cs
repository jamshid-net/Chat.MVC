using Microsoft.AspNetCore.Authentication.Cookies;

namespace Chat.MVC.Configurations;

public static class AuthorizeConfigure
{
    public static IServiceCollection AddCookieAuthentication(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(configuration.GetValue<double>("Cookie:ExpireTimeInMinutes"));
                options.Cookie.MaxAge = options.ExpireTimeSpan;
                options.SlidingExpiration = true;

            });

        
        return services;
    }
}
