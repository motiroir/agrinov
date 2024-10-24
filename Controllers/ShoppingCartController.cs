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
            ShoppingCart currentShoppingCart = null;
            using (ServiceUserAccount sUA = new ServiceUserAccount())
            {
                int userAccountId = Int32.Parse(HttpContext.User.Identity.Name);
                if (!sUA.CheckIfMemberShipValid(userAccountId))
                {
                    using (IServiceShoppingCart sSC = new ServiceShoppingCart())
                    {
                        if (!sSC.IsAMemberShipFeeInTheCart(userAccountId))
                        {
                            sSC.AddMemberShipFeeToShoppingCart(userAccountId, new ShoppingCartItem());
                        }
                        currentShoppingCart = sSC.GetShoppingCartForUserAccount(HttpContext.User.Identity.Name);
                    }
                }
            }
            if (currentShoppingCart == null)
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
            using (ServiceShoppingCart sSC = new ServiceShoppingCart())
            {
                currentShoppingCart = sSC.GetShoppingCartForUserAccount(HttpContext.User.Identity.Name);
            }
            if (currentShoppingCart == null)
            {
                return View("Error");
            }
            using (IServiceShoppingCart sSC = new ServiceShoppingCart())
            {
                for (int i = 0; i < currentShoppingCart.ShoppingCartItems.Count; i++)
                {
                    //checking if product quantity was changed for a shoppingcartitem
                    if (currentShoppingCart.ShoppingCartItems[i].Product != null && currentShoppingCart.ShoppingCartItems[i].Quantity != shoppingCart.ShoppingCartItems[i].Quantity)
                    {
                        //Ignore Change if input superior to product stock
                        if (!(shoppingCart.ShoppingCartItems[i].Quantity > currentShoppingCart.ShoppingCartItems[i].Product.Stock))
                        {
                            sSC.AddProductToShoppingCart(currentShoppingCart.ShoppingCartItems[i].Product.Id, shoppingCart.ShoppingCartItems[i].Quantity, currentShoppingCart.UserAccountId);
                        }
                    }
                }
            }
            return RedirectToAction("Index", "ShoppingCart");
        }

        [Authorize]
        [HttpGet]
        public IActionResult PlaceOrder()
        {
            //Check stocks before proceeding to payment
            //TODO
            Payment payment = new Payment();
            //Select cash by default
            payment.PaymentType = PaymentType.CASH;
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult PlaceOrder(Payment payment)
        {
            payment.Date = DateTime.Now;
            // to implement online payment, check if payment online and redirect before setting to true
            if (payment.PaymentType == PaymentType.CARD)
            {
                payment.Received = true;
            }
            // the stocks will be decreased when writing order, so you need to be sure the payment is received
            using (IServiceOrder sO = new ServiceOrder())
            {
                sO.SaveShoppingCartAsAnOrder(HttpContext.User.Identity.Name, payment);
            }
            return RedirectToAction("Index", "ShoppingCart");
        }

        [Authorize]
        [HttpGet]
        public IActionResult EmptyCart()
        {
            using (IServiceShoppingCart sSC = new ServiceShoppingCart())
            {
                sSC.EmptyShoppingCartExceptMemberShipFee(HttpContext.User.Identity.Name);
            }
            return RedirectToAction("Index", "ShoppingCart");
        }


    }
}