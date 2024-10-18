using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgriNov.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriNov.Controllers
{
    public class MyAccountController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            UserAccount userAccount;
            using(IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                userAccount = serviceUserAccount.GetUserAccountByID(HttpContext.User.Identity.Name);
            }
            return View(userAccount);
        }
    }
}