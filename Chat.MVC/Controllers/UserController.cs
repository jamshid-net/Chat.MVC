using Chat.Application.UseCases.Users.Commands;
using Chat.Application.UseCases.Users.Queries;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using X.PagedList;

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

    [HttpPost] 
    public async ValueTask<IActionResult> Register([FromForm] UserRegisterCommand register)
    {
        if(ModelState.IsValid)
        {
            var claimsIdentity = await mediator.Send(register);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("ChatPage");

        }
        return RedirectToAction("Error", "Home");
        
    }



    [Authorize(Roles = "admin")]
    [HttpPost]
    public async ValueTask<IActionResult> Update([FromForm] UserUpdateCommand update)
    {
        if (ModelState.IsValid)
        {
           await mediator.Send(update);
           return RedirectToAction("AdminMainPage","Admin");
        }
        return RedirectToAction("Error", "Home");
    }
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async ValueTask<IActionResult> Create([FromForm] UserCreateCommand create)
    {
        if (ModelState.IsValid)
        {
            await mediator.Send(create);
            return NoContent();
          //  return RedirectToAction("AdminMainPage", "Admin");
        }
        return RedirectToAction("Error", "Home");
    }




    [HttpPost]
    [Authorize(Roles = "admin")]
    public async ValueTask<IActionResult> Delete([FromForm] UserDeleteCommand delete)
    {
        if (ModelState.IsValid)
        {
            await mediator.Send(delete);
            return NoContent();
            
        }
        return RedirectToAction("Error", "Home");
    }




   

    public IActionResult LoginPage()
    {
        return View();
    }
    public async ValueTask<IActionResult> RegisterPage()
    {
        return await ValueTask.FromResult(View());
    }


    [Authorize]
    public IActionResult ChatPage()
    {
        return View();
    }
}
