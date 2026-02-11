using Microsoft.AspNetCore.Mvc;

namespace WebSpark.PrismSpark.Demo.Controllers;

public class JokeController : Controller
{
    public IActionResult Index()
    {

        return View();
    }
}
