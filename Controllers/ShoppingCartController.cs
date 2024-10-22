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
        [HttpGet]
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

        [Authorize]
        [HttpPost]
        public IActionResult Index(ShoppingCart shoppingCart)
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
            using(IServiceShoppingCart sSC = new ServiceShoppingCart())
            {
                for(int i = 0; i < currentShoppingCart.ShoppingCartItems.Count ;i++)
                {
                    if(currentShoppingCart.ShoppingCartItems[i].Product != null || currentShoppingCart.ShoppingCartItems[i].Quantity != shoppingCart.ShoppingCartItems[i].Quantity)
                    {
                        sSC.AddProductToShoppingCart(currentShoppingCart.ShoppingCartItems[i].Product.Id, shoppingCart.ShoppingCartItems[i].Quantity, currentShoppingCart.UserAccountId);
                    }
                }
            }
            return RedirectToAction("Index","ShoppingCart");
        }

        public IActionResult PlaceOrder()
        {
            return View(); //Ajouter le form de paiement
        }

    }
}