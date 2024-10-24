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
        public IActionResult LoginWithSignUpSlide()
        {
            LoginWithSignup viewModel = new LoginWithSignup() { UserAccountLogin = new UserAccountLogin(), UserAccountCreation = new UserAccountCreation()};
            viewModel.UserAccountLogin.IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            if (viewModel.UserAccountLogin.IsAuthenticated)
            {
                using (ServiceUserAccount sUA = new ServiceUserAccount())
                {
                    viewModel.UserAccountLogin.UserAccount = sUA.GetUserAccountByID(HttpContext.User.Identity.Name);
                }
            }
            ViewData["FromAccountCreation"] = false;
            return View(viewModel);
        }

		// [HttpGet]
        // public IActionResult CreateAccount()
        // {
        //     return View();
        // }

        [HttpPost]
        public IActionResult CreateAccount(LoginWithSignup bigViewModel)
        {
            if (bigViewModel.UserAccountCreation.UserAccount.Password.Equals(bigViewModel.UserAccountCreation.ConfirmPassword))
            {
                if (ModelState.IsValid)
                {
                    using (ServiceUserAccount sUA = new ServiceUserAccount())
                    {
                        sUA.InsertUserAccount(bigViewModel.UserAccountCreation.UserAccount);
                        return RedirectToAction("Index", "MyAccount");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("UserAccountCreation.ConfirmPassword", "Les mots de passe ne correspondent pas.");
            }
            bigViewModel.UserAccountLogin = new UserAccountLogin();
            bigViewModel.UserAccountLogin.IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            ViewData["FromAccountCreation"] = true;
            return View("LoginWithSignUpSlide", bigViewModel);
        }

        // [HttpGet]
        // public IActionResult Login()
        // {
        //     UserAccountLogin viewModel = new UserAccountLogin() { IsAuthenticated = HttpContext.User.Identity.IsAuthenticated };
        //     if (viewModel.IsAuthenticated)
        //     {
        //         using (ServiceUserAccount sUA = new ServiceUserAccount())
        //         {
        //             viewModel.UserAccount = sUA.GetUserAccountByID(HttpContext.User.Identity.Name);
        //         }
        //     }
        //     return View(viewModel);
        // }

        [HttpPost]
        public IActionResult Login(LoginWithSignup bigViewModel)
        {
            if (ModelState.IsValid)
            {
                using (ServiceUserAccount sUA = new ServiceUserAccount())
                {
                    UserAccount userAccount = sUA.Authenticate(bigViewModel.UserAccountLogin.UserAccount.Mail, bigViewModel.UserAccountLogin.UserAccount.Password);
                    if (userAccount != null)
                    {
                        List<Claim> userClaims = new List<Claim>() {
                            new Claim(ClaimTypes.Name, userAccount.Id.ToString()),
                            new Claim(ClaimTypes.Role, userAccount.UserAccountLevel.ToString())
                        };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(userClaims, "User Identity");
                        ClaimsPrincipal userPrincipal = new ClaimsPrincipal(new[] { claimsIdentity });

                        HttpContext.SignInAsync(userPrincipal);
                        // Moved to ShoppingCart/Index
                        // if(!sUA.CheckIfMemberShipValid(userAccount.Id))
                        // {
                        //     using(IServiceShoppingCart sSC = new ServiceShoppingCart())
                        //     {
                        //         if(!sSC.IsAMemberShipFeeInTheCart(userAccount.Id)){
                        //             sSC.AddMemberShipFeeToShoppingCart(userAccount.Id, new ShoppingCartItem());
                        //         }
                        //     }
                        // }

                        return RedirectToAction("Index","DashBoard"); 
                    }
                    else
                    {
                        ModelState.AddModelError("UserAccountLogin.UserAccount.Mail", "Identifiant et/ou mot de passe incorrect(s)");
                    }
                }
            }
            ViewData["FromAccountCreation"] = false;
            return View("LoginWithSignUpSlide",bigViewModel);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/Home/Index");
        }

    }

}
