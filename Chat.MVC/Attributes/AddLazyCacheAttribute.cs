using LazyCache;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chat.MVC.Attributes;

public class AddLazyCacheAttribute:ActionFilterAttribute
{
    //private static IAppCache? appCache;
    //private static TimeSpan duration = TimeSpan.FromSeconds(100);
    //public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    //{
    //    appCache = context.HttpContext.RequestServices.GetRequiredService<IAppCache>();

    //    var cachedResult = await appCache.GetOrAddAsync("users", () => next(), duration);
    //    if (cachedResult is not null)
    //        context.Result = cachedResult.Result;


    //}

}
