using Microsoft.AspNetCore.Mvc;

namespace AgriNov.Controllers
{
    public class ProductionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
