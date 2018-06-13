using Microsoft.AspNetCore.Mvc;

namespace TinyTimeline.Controllers
{
    public class SessionsController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}