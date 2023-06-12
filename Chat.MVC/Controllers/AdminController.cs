using Chat.Application.UseCases.Admin.Commands;
using Chat.Application.UseCases.Users.Queries;
using Chat.MVC.Attributes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using X.PagedList;

namespace Chat.MVC.Controllers;
public class AdminController : BaseController
{
    [EnableRateLimiting("FixedLimiter")]
    public IActionResult AdminLoginPage()
    {
        bool isLoggedIn = true;
        ViewBag.IsLoggedIn = isLoggedIn;
        return View();
    }



    [EnableRateLimiting("FixedLimiter")]
    [HttpPost]
    public async ValueTask<IActionResult> Login([FromForm] AdminLoginCommand command)
    {
        var claimsIdentity = await mediator.Send(command);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        return RedirectToAction("AdminMainPage");
    }

    [EnableRateLimiting("FixedLimiter")]
    [HttpGet]
    public async ValueTask<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }


   
    [EnableRateLimiting("SlidingLimiter")]
    [Authorize(Roles = "admin")]
    public async ValueTask<IActionResult> AdminMainPage(int? page=1)
    {

        var pageNumber = page ?? 1;
        int pageSize = 6;
        var onePageOfUsers = (await mediator.Send(new UserGetAllQuery())).ToPagedList(pageNumber, pageSize);
        

        return await ValueTask.FromResult(View(onePageOfUsers));
    }


}
