using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models
{
    public class Production
    {
        public int Id { get; set; }

        
        public string CompanyName { get; set; }

        [Required]
        [DisplayName("Quantité par semaine (au kg / Litre / Pièce)")]
        [Range(1, int.MaxValue, ErrorMessage = "Le volume doit être supérieur à 0.")]
        public int VolumePerDelivery { get; set; }

        [Required]
        [DisplayName("Prix (en €)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0.")]
        public decimal Price { get; set; }

        
        public ValidationStatus ValidationStatus { get; set; }

        [Required]
        [DisplayName("Type de produit")]
        public ProductType ProductType { get; set; }

        [MaxLength(500)]
        
        public string Description { get; set; }

        [Required]
        [DisplayName("Saison")]
        public Seasons Seasons { get; set; }

        [Required]
        [DisplayName("Année")]
        public Years Years { get; set; }

        [Required]
        [DisplayName("Fréquence de livraison")]
        public DeliveryFrequency DeliveryFrequency { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }

        public int SupplierId { get; set; }
        
    }
}
