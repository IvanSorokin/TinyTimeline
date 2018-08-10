using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace TinyTimeline.Controllers
{
    public class TokensController : Controller
    {
        private readonly string[] availableHosts;

        public TokensController(IConfiguration config)
        {
            availableHosts = config.GetSection("AvailableRedirectHosts").Get<string[]>();
        }

        public IActionResult SetToken(string returnUrl=null)
        {
            return View("SetToken", returnUrl);
        }
        
        [HttpPost]
        public IActionResult SaveToken(string token, Uri returnUrl)
        {
            Response.Cookies.Append("authToken", token, new CookieOptions{Expires = DateTimeOffset.Now.AddYears(1)});
            
            if (returnUrl != null && availableHosts.Any(z => returnUrl.Host == z))
                return Redirect(returnUrl.AbsoluteUri);
            
            return RedirectToAction("Sessions", "Presentation");
        }
    }
}