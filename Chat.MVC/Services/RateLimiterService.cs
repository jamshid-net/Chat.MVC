using Microsoft.AspNetCore.RateLimiting;

namespace Chat.MVC.Services;

public static class RateLimiterService
{
    public static IServiceCollection AddRateLimiterService(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.AddSlidingWindowLimiter("SlidingLimiter", opt =>
            {
                opt.Window = TimeSpan.FromSeconds(12);
                opt.QueueLimit = 10;
                opt.PermitLimit = 15;
                opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                opt.AutoReplenishment = true;
                opt.SegmentsPerWindow = 2;


            });

            options.AddFixedWindowLimiter("FixedLimiter", opt =>
            {
                opt.Window = TimeSpan.FromSeconds(8);
                opt.PermitLimit = 10;
                
                opt.AutoReplenishment= true;
              

            });

            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.HttpContext.Response.WriteAsync("Too many requests. Please try later again... ", cancellationToken: token);
            };

        });

        return services;
    }
}
