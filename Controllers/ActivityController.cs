using AgriNov.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgriNov.Controllers
{
    public class ActivityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateActivity()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateActivity(Activity activity)
        {
            if(ModelState.IsValid)
            {
                using(ServiceActivity sA = new ServiceActivity()) 
                {
                    sA.InsertActivity(activity);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}
