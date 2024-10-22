using AgriNov.Models;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AgriNov.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult CreateProduct()
        {
            return View();
        }

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
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(product);
        }

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
                        return RedirectToAction("ProductDashboard", "Product");
                    }
                }
            }
            return View(product);
        }
       
        public IActionResult DeleteProduct(int id)
        {
            return View();
        }

       public IActionResult ShowAllProducts(ProductViewModel pVM)
        {
            return View(pVM);
        }

        public IActionResult ProductDashboard(string activeTab = "ShowAllProducts")
        {
            ProductViewModel pVM = new ProductViewModel();
            int userId = int.Parse(HttpContext.User.Identity.Name);

            using (ProductService sP = new ProductService())
            {
                // getting all products in viewmodel
                pVM.AllProducts = sP.GetProducts();

            }

            ViewData["ActiveTab"] = activeTab;
            return View(pVM);
        }

        public IActionResult ShowProductDetails(int id)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            ProductViewModel pVM = new ProductViewModel();
            using (ProductService sP= new ProductService())
            {
                pVM.Product = sP.GetProductByID(id);
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

        [HttpPost]
        public IActionResult AddToCart(Product product, int quantity)
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
            return RedirectToAction("ProductDashboard", "Product");
        }
    }
}