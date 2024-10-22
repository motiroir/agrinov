using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Veuillez indiquer un nom")]
        [DisplayName("Nom du produit")]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Veuillez indiquer un prix")]
        [DisplayName("Prix (en €)")]
        [Range(0.01, decimal.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0")]
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        [Required(ErrorMessage = "Veuillez indiquer une date d'expiration")]
        [DisplayName("Date d'expiration")]
        public DateTime ExpirationDate { get; set; }
        [Required]
        [MaxLength(500)]
        [MinLength(20, ErrorMessage = "Veuillez décrire le produit")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Veuillez indiquer un stock")]
        [DisplayName("Stock")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Veuillez indiquer une catégorie")]
        [DisplayName("Catégorie")]
        public string Category { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

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


