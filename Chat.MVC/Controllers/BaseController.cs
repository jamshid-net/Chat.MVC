using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chat.MVC.Controllers;
public class BaseController : Controller
{
    
    protected  IMediator mediator
        => HttpContext.RequestServices.GetRequiredService<IMediator>();

}
