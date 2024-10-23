using System.Security.Claims;
using AgriNov.Models;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

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
                        return RedirectToAction("Index", "MyAccount");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("UserAccountCreation.ConfirmPassword", "Les mots de passe ne correspondent pas.");
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            UserAccountLogin viewModel = new UserAccountLogin() { IsAuthenticated = HttpContext.User.Identity.IsAuthenticated };
            if (viewModel.IsAuthenticated)
            {
                using (ServiceUserAccount sUA = new ServiceUserAccount())
                {
                    viewModel.UserAccount = sUA.GetUserAccountByID(HttpContext.User.Identity.Name);
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Login(UserAccountLogin viewModel)
        {
            if (ModelState.IsValid)
            {
                using (ServiceUserAccount sUA = new ServiceUserAccount())
                {
                    UserAccount userAccount = sUA.Authenticate(viewModel.UserAccount.Mail, viewModel.UserAccount.Password);
                    if (userAccount != null)
                    {
                        List<Claim> userClaims = new List<Claim>() {
                            new Claim(ClaimTypes.Name, userAccount.Id.ToString()),
                            new Claim(ClaimTypes.Role, userAccount.UserAccountLevel.ToString())
                        };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(userClaims, "User Identity");
                        ClaimsPrincipal userPrincipal = new ClaimsPrincipal(new[] { claimsIdentity });

                        HttpContext.SignInAsync(userPrincipal);
                        if(!sUA.CheckIfMemberShipValid(userAccount.Id))
                        {
                            using(IServiceShoppingCart sSC = new ServiceShoppingCart())
                            {
                                if(!sSC.IsAMemberShipFeeInTheCart(userAccount.Id)){
                                    sSC.AddMemberShipFeeToShoppingCart(userAccount.Id, new ShoppingCartItem());
                                }
                            }
                        }
                        return RedirectToAction("Index","DashBoard"); 
                    }
                    else
                    {
                        ModelState.AddModelError("UserAccount.Mail", "Identifiant et/ou mot de passe incorrect(s)");
                    }
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/Home/Index");
        }

    }

}
