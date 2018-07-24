using Microsoft.AspNetCore.Mvc;

namespace TinyTimeline.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => RedirectToAction("Sessions", "Presentation");
    }
}