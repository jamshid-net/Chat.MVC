﻿using Chat.Application.UseCases.Admin.Commands;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Chat.MVC.Controllers;
public class AdminController : BaseController
{
    public IActionResult AdminLoginPage()
    {
        return View();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Login([FromForm] AdminLoginCommand command)
    {
        var claimsIdentity = await mediator.Send(command);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        return RedirectToAction("AdminMainPage");
    }

    [Authorize(Roles ="admin")]
    public  async ValueTask<IActionResult> AdminMainPage()
    {
        return await ValueTask.FromResult(View());
    }
}