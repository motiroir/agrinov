using AgriNov.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriNov.Controllers
{
    public class DashBoard : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
           UserAccount userAccount;
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                userAccount = serviceUserAccount.GetUserAccountByID(HttpContext.User.Identity.Name);
            }


                return View(userAccount);
        }
    }
}
