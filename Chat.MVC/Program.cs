using Chat.Application;
using Chat.Infrastructure;
using Chat.MVC.Configurations;

namespace Chat.MVC;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

       
        builder.Services.AddControllersWithViews();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddChatMVCServices();
        builder.Services.AddApplication();
        builder.Services.AddCookieAuthentication(builder.Configuration);

        var app = builder.Build();

      
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
