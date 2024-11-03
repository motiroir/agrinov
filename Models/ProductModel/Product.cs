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
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0")]
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
        //Actually the UserAccountId of the Supplier
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public byte[] ImgProduct { get; set; }
    }

}


