using Chat.Application.Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Chat.MVC.Middlewares;
// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
         await _next(httpContext);
           
        }
        catch (NotFoundException ex)
        {
            await HandleException(httpContext, ex.Message, StatusCodes.Status404NotFound, ex.Message);
        }


        catch (Exception ex)
        {
            await HandleException(httpContext, ex.Message, StatusCodes.Status500InternalServerError, ex.Message);
        }

    }

    private async ValueTask<IActionResult> HandleException(HttpContext httpContext, string message, int statuscode, string message2)
    {

        Log.Error("EXCEPTION:🔴 CLIENT_IP:{ClientIp}" + $"\nDatetime:{DateTime.Now} | Message:{message} | Path:{httpContext.Request.Path}");
        HttpResponse response = httpContext.Response;
        response.ContentType = "application/json";
        response.StatusCode = statuscode;
        var path = httpContext.Request.Headers["Referer"].ToString();

        //var error = new
        //{
        //    Message = message,
        //    StatusCode = statuscode
        //};
        
        return await Task.FromResult(new RedirectToActionResult("Index", "Home",null));
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class GlobalExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionMiddleware>();
    }
}
