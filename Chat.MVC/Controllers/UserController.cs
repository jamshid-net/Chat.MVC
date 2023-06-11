using Chat.Application.UseCases.Users.Commands;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Chat.MVC.Controllers;
public class UserController : BaseController
{
    [HttpPost]
    public async ValueTask<IActionResult> Login([FromForm] UserLoginCommand login)
    {
        var claimsIdentity =  await  mediator.Send(login);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        return RedirectToAction("ChatPage");

    }

    public IActionResult LoginPage()
    {
        return View();
    }
    public IActionResult AllUser()
    {

        return View();
    }

    [Authorize]
    public IActionResult ChatPage()
    {
        return View();
    }
}
