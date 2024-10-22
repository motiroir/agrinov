using AgriNov.Models;

namespace AgriNov.ViewModels
{
    public class ProductViewModel
    {
        public List<Product> AllProducts { get; set; }
        public Product Product { get; set; }
        public int QuantityByProductInCart { get; set; }
    }
}
