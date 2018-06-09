using Microsoft.AspNetCore.Mvc;


namespace TinyTimeline.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}