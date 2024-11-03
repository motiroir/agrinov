using Microsoft.AspNetCore.Mvc;

namespace AgriNov.Controllers
{
       public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LegalNotice()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

    }
}