using Chat.Application.UseCases.Users.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Chat.MVC.Controllers;
public class UserController : BaseController
{
    [HttpPost]
    //public ValueTask<IActionResult> Login([FromForm] UserLoginCommand login)
    //{

    //}

    public IActionResult AllUser()
    {
        return View();
    }
}
