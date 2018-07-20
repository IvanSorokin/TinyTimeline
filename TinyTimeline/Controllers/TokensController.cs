using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TinyTimeline.Controllers
{
    public class TokensController : Controller
    {
        public IActionResult SetToken()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult SaveToken(string token)
        {
            Response.Cookies.Append("authToken", token, new CookieOptions{Expires = DateTimeOffset.Now.AddYears(1)});
            return RedirectToAction("Sessions", "Presentation");
        }
    }
}