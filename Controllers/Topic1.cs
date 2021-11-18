using Microsoft.AspNetCore.Mvc;

namespace OWASP10_2021.Controllers
{
    public class Topic1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
