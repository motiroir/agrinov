﻿using AgriNov.Models;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriNov.Controllers
{
    
    public class ProductController : Controller
    {
        [Authorize(Roles = "SUPPLIER")]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [Authorize(Roles = "SUPPLIER")]
        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                int userId = int.Parse(HttpContext.User.Identity.Name);
                product.SupplierId= userId;
                using (ProductService sP = new ProductService())
                {
                    sP.InsertProduct(product);
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            return View(product);
        }

        [Authorize(Roles = "SUPPLIER")]
        public IActionResult UpdateProduct(int id)
        {
            if (id > 0)
            {
                using (ProductService sP = new ProductService())
                {
                    Product oldProduct = sP.GetProductByID(id);
                    if (oldProduct != null)
                    {
                        return View(oldProduct);
                    }
                }
            }
            return View("Error");
        }

        [Authorize(Roles = "SUPPLIER")]
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Id > 0)
                {
                    using (ProductService sP = new ProductService())
                    {
                        sP.UpdateProduct(product);
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
            }
            return View(product);
        }

        [Authorize(Roles = "SUPPLIER")]
        public IActionResult DeleteProduct(int id)
        {
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        public IActionResult ShowAllProducts(ProductViewModel pVM)
        {
            return View(pVM);
        }

        [Authorize( Roles = "ADMIN,VOLUNTEER,USER,SUPPLIER,CORPORATE")]
        public IActionResult ProductDashboard(string activeTab = "ShowAllProducts")
        {
            ProductViewModel pVM = new ProductViewModel();
            int userId = int.Parse(HttpContext.User.Identity.Name);

            // getting all products in viewmodel
            using (ProductService sP = new ProductService())
            {
                pVM.AllProducts = sP.GetProducts();
            }
            // getting all relavant box contracts for sale in viewmodel
            using (ServiceBoxContract sBC = new ServiceBoxContract())
            {
                pVM.AllBoxContractsToSale = sBC.GetAllAvailableBoxContracts(userId);
                pVM.MyCurrentBoxContracts = sBC.GetCurrentBoxContractsForUser(userId);
                if(pVM.AllBoxContractsToSale.Count > 0)
                {
                    List<DateTime> startEndSeason = sBC.ComputeSeasonStartAndEnd(pVM.AllBoxContractsToSale[0].Years,pVM.AllBoxContractsToSale[1].Seasons);
                    pVM.NextSeasonInfo = $"La prochaine saison {pVM.AllBoxContractsToSale[0].Seasons.GetDisplayName()} {pVM.AllBoxContractsToSale[0].Years.GetDisplayName()} débutera le {startEndSeason[0]:dd/MM/yyyy} et se terminera le {startEndSeason[1]:dd/MM/yyyy}.";
                }
            }

            ViewData["ActiveTab"] = activeTab;
            return View(pVM);
        }

        [Authorize(Roles = "ADMIN,VOLUNTEER,USER,SUPPLIER,CORPORATE")]
        public IActionResult ShowProductDetails(int id)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            ProductViewModel pVM = new ProductViewModel();
            using (ProductService sP= new ProductService())
            {
                pVM.Product = sP.GetProductByID(id);
                pVM.SupplierName = sP.GetSupplierName(pVM.Product.SupplierId);
            }
            using(ServiceShoppingCart sSC = new ServiceShoppingCart())
            {
                pVM.QuantityByProductInCart = sSC.GetQuantityByProductInCart(id, userId);
            }
            if (pVM.Product == null)
            {
                return View("Error"); 
            }
            return View(pVM);
        }

        [Authorize(Roles = "ADMIN,VOLUNTEER,USER,SUPPLIER,CORPORATE")]
        [HttpPost]
        public IActionResult AddProductToCart(Product product, int quantity)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            if (quantity <= 0 || quantity > product.Stock)
            {
                return RedirectToAction("ShowProductDetails", "Product", new { id = product.Id });
            }
            using (ServiceShoppingCart sSC = new ServiceShoppingCart())
            {
                sSC.AddProductToShoppingCart(product.Id, quantity, userId);
            }

            TempData["SuccessMessage"] = "L'article a bien été ajouté au panier.";
            return RedirectToAction("ProductDashboard", "Product");
        }

        [Authorize(Roles = "ADMIN,VOLUNTEER,USER,SUPPLIER,CORPORATE")]
        [HttpPost]
        public IActionResult AddBoxContractToCart(int boxContractId, int quantity)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            //for now, only weekly big or small boxes are allowed
            if(!(quantity ==2 || quantity == 1))
            {
                return View("Error");
            }
            // check if input Id was not modified to an unauthorized one
            using(IServiceBoxContract sBC = new ServiceBoxContract())
            {
                BoxContract boxContract = sBC.GetBoxContractById(boxContractId);
                if(boxContract == null || !boxContract.ForSale)
                {
                    return View("Error");
                }
            }
            using (IServiceShoppingCart sSC = new ServiceShoppingCart())
            {
                sSC.AddBoxContractToShoppingCart(boxContractId, quantity, userId);
            }

            TempData["SuccessMessage"] = "L'abonnement a bien été ajouté au panier.";
            return RedirectToAction("ProductDashboard", "Product", new { activeTab = "ShowMyBox" });
        }


    }
}