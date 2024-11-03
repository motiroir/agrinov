using AgriNov.Models;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriNov.Controllers
{
    public class ShoppingCartController : Controller
    {
        [Authorize(Roles = "USER,CORPORATE,SUPPLIER,VOLUNTEER,ADMIN")]
        [HttpGet]
        public IActionResult Index()
        {
            ShoppingCart currentShoppingCart = null;
            using (ServiceUserAccount sUA = new ServiceUserAccount())
            {
                using (IServiceShoppingCart sSC = new ServiceShoppingCart())
                {
                    int userAccountId = Int32.Parse(HttpContext.User.Identity.Name);
                    if (!sUA.CheckIfMemberShipValid(userAccountId))
                    {

                        if (!sSC.IsAMemberShipFeeInTheCart(userAccountId))
                        {
                            sSC.AddMemberShipFeeToShoppingCart(userAccountId, new ShoppingCartItem());
                        }

                    }
                    currentShoppingCart = sSC.GetShoppingCartForUserAccount(HttpContext.User.Identity.Name);
                }
            }
            if (currentShoppingCart == null)
            {
                return View("Error");
            }
            return View(currentShoppingCart);
        }

        [Authorize(Roles = "USER,CORPORATE,SUPPLIER,VOLUNTEER,ADMIN")]
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
                    //checking if product quantity was changed for a shoppingcartitem, the form does not allow changing other quantities
                    if (i < shoppingCart.ShoppingCartItems.Count &&
                        currentShoppingCart.ShoppingCartItems[i].Product != null &&
                        currentShoppingCart.ShoppingCartItems[i].Quantity != shoppingCart.ShoppingCartItems[i].Quantity)
                    {
                        int requestedQuantity = shoppingCart.ShoppingCartItems[i].Quantity;
                        int availableStock = currentShoppingCart.ShoppingCartItems[i].Product.Stock;

                        //Ignore Change if input superior to product stock
                        if (requestedQuantity <= availableStock && requestedQuantity >= 0)
                        {
                            sSC.AddProductToShoppingCart(
                                currentShoppingCart.ShoppingCartItems[i].Product.Id,
                                requestedQuantity,
                                currentShoppingCart.UserAccountId
                            );
                        }
                    }
                }
            }
            return RedirectToAction("Index", "ShoppingCart");
        }

        [Authorize(Roles = "USER,CORPORATE,SUPPLIER,VOLUNTEER,ADMIN")]
        [HttpGet]
        public IActionResult PlaceOrder()
        {
            PaymentViewModel viewModel = new PaymentViewModel
            {
                Payment = new Payment(), 
                ShoppingCart = null
            };

            using (ServiceUserAccount sUA = new ServiceUserAccount())
            {
                using (IServiceShoppingCart sSC = new ServiceShoppingCart())
                {
                    int userAccountId = Int32.Parse(HttpContext.User.Identity.Name);

                    if (!sUA.CheckIfMemberShipValid(userAccountId))
                    {
                        if (!sSC.IsAMemberShipFeeInTheCart(userAccountId))
                        {
                            sSC.AddMemberShipFeeToShoppingCart(userAccountId, new ShoppingCartItem());
                        }
                    }

                    viewModel.ShoppingCart = sSC.GetShoppingCartForUserAccount(HttpContext.User.Identity.Name);
                }
            }

            if (viewModel.ShoppingCart == null)
            {
                return View("Error");
            }

            return View(viewModel);
        }

        [Authorize(Roles = "USER,CORPORATE,SUPPLIER,VOLUNTEER,ADMIN")]
        [HttpPost]
        public IActionResult PlaceOrder(PaymentViewModel viewModel, string action)
        {
            using (ServiceUserAccount sUA = new ServiceUserAccount())
            {
                using (IServiceShoppingCart sSC = new ServiceShoppingCart())
                {
                    int userAccountId = Int32.Parse(HttpContext.User.Identity.Name);

                    if (!sUA.CheckIfMemberShipValid(userAccountId))
                    {
                        if (!sSC.IsAMemberShipFeeInTheCart(userAccountId))
                        {
                            sSC.AddMemberShipFeeToShoppingCart(userAccountId, new ShoppingCartItem());
                        }
                    }

                    viewModel.ShoppingCart = sSC.GetShoppingCartForUserAccount(HttpContext.User.Identity.Name);
                }
            }

            if (action == "online")
            {
                if (ModelState.IsValid)
                {
                    Payment payment = new Payment();
                    payment.Date = DateTime.Now;
                    payment.PaymentType = PaymentType.CARD;
                    payment.Received = true;

                    viewModel.Payment = payment;

                    using (IServiceOrder sO = new ServiceOrder())
                    {
                        sO.SaveShoppingCartAsAnOrder(HttpContext.User.Identity.Name, viewModel.Payment);
                    }

                    return RedirectToAction("ShowMyOrders", "Order");
                }
            }
            if (action == "offline")
            {
                Payment payment = new Payment();
                payment.PaymentType = PaymentType.NA;
                payment.Received = false;

                viewModel.Payment = payment;

                using (IServiceOrder sO = new ServiceOrder())
                {
                    sO.SaveShoppingCartAsAnOrder(HttpContext.User.Identity.Name, viewModel.Payment);
                }

                return RedirectToAction("ShowMyOrders", "Order");
            }

                return View(viewModel);
        }

        [Authorize(Roles = "USER,CORPORATE,SUPPLIER,VOLUNTEER,ADMIN")]
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