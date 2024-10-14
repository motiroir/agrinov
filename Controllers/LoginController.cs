using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AgriNov.Models;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AgriNov.Controllers
{

    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(UserAccountCreation viewModel)
        {
            if (viewModel.UserAccount.Password.Equals(viewModel.ConfirmPassword))
            {
                if (ModelState.IsValid)
                {
                    using (ServiceUserAccount sUA = new ServiceUserAccount())
                    {
                        sUA.InsertUserAccount(viewModel.UserAccount);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("UserAccountCreation.ConfirmPassword", "Les mots de passe ne correspondent pas.");
            }
            return View(viewModel);
        }

    }

}
