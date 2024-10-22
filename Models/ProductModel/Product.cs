using System;
using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models
{
    public class Product
    {
     

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }

        internal static List<Product> GetProduct()
        {
           
                // Simulate a data source (e.g., database or in-memory list of products)
                List<Product> products = new List<Product>
    {
        new Product { Id = 1, Name = "Carotte", Price = 10, Description = "Description of Product A", Stock=10 },
        new Product { Id = 2, Name = "Tomate", Price = 15, Description = "Description of Product B",  Stock=10 },
        new Product { Id = 3, Name = "Navet", Price = 8, Description = "Description of Product C", Stock=10}
    };

                // Return the list of products
                return products;
            }
        }
}


