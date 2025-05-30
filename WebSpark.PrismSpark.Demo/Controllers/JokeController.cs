using Microsoft.AspNetCore.Mvc;
using WebSpark.HttpClientUtility.RequestResult;

namespace WebSpark.PrismSpark.Demo.Controllers;

public class JokeController(IHttpRequestResultService service) : Controller
{
    public IActionResult Index()
    {

        return View();
    }
}
