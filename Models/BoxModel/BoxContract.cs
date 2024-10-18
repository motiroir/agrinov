using AgriNov.Models.ProductionModel;
using AgriNov.Models.SharedStatus;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AgriNov.Models
{
	public class BoxContract
	{
		public int Id { get; set; }

        [Required]
        [DisplayName("Type de produit")]
        public ProductType ProductType { get; set; }

        [Required]
        [DisplayName("Saison assginée")]
        public Seasons Seasons { get; set; }

        [Required]
        [DisplayName("Année assginée")]
        public Years Years { get; set; }

        [Required]
        [DisplayName("Prix")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0.")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Nombre maximum d'abonnements")]
        public int MaxSubscriptions { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }

    }


}
