using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AgriNov.Models;
using AgriNov.Models.UserAccountModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AgriNov.Controllers
{
    public class AccountController : Controller
    {
        [Authorize(Roles = "DEFAULT")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "DEFAULT")]
        public IActionResult TypeSelection(UserAccount userAccount)
        {
            switch (userAccount.UserAccountLevel)
            {
                case UserAccountLevel.USER:
                    return RedirectToAction("AddRegularUser", "Account");
                // case UserAccountLevel.CORPORATE:
                //     return "You want to be a corporate user";
                // case UserAccountLevel.SUPPLIER:
                //     return "You want to be a supplier";
                default:
                    return View();
            }
        }

        [Authorize(Roles = "DEFAULT")]
        [HttpGet]
        public IActionResult AddRegularUser()
        {
            User user = new User();
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                user.UserAccount = serviceUserAccount.GetUserAccountByID(HttpContext.User.Identity.Name);
            }
            return View(user);
        }

        [Authorize(Roles = "DEFAULT")]
        [HttpPost]
        public IActionResult AddRegularUser(User user)
        {
            // Check if logged user id was not modified, if not proceed otherwise error.
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                UserAccount currentUserAccount = serviceUserAccount.GetUserAccountByID(HttpContext.User.Identity.Name);
                if (user.UserAccount.Id.Equals(currentUserAccount.Id))
                {
                    user.UserAccount = currentUserAccount;
                }
                else
                {
                    return View("Error");
                }
            }
            if (!ModelState.IsValid)
            {
                using (IServiceUser serviceUser = new ServiceUser())
                {
                    serviceUser.InsertUser(user);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(user);
        }
    }
}
