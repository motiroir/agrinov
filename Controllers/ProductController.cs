using AgriNov.Models;

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
                    Product oldProduct = sP.GetProduct(id);
                    if (oldProduct != null)
                    {
                        return View(oldProduct);
                    }
                }
            }
            return View("NotFound");
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
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(product);
        }
       
        public IActionResult DeleteProduct(int id)
        {
                         return View();
         
        }

       public IActionResult ShowAllProducts()
        {
            // Assuming you have a ProductService that fetches the products from a data source
            List<Product> products = Product.GetProduct();

            // Passing the list of products to the view
            return View(products);
        }
    }
}