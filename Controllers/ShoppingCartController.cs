using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AgriNov.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AgriNov.Controllers
{
    public class ShoppingCartController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            ShoppingCart currentShoppingCart;
            using(ServiceShoppingCart sSC = new ServiceShoppingCart())
            {
                currentShoppingCart = sSC.GetShoppingCartForUserAccount(HttpContext.User.Identity.Name);
            }
            if(currentShoppingCart == null)
            {
                return View("Error");
            }
            return View(currentShoppingCart);
        }

    }
}