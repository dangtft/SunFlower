using Microsoft.AspNetCore.Mvc;

namespace WebSunFlower.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
