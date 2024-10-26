using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AgriNov.Models
{
	public class BoxContract
	{
		public int Id { get; set; }
        public ProductType ProductType { get; set; }
        public Seasons Seasons { get; set; }
        public Years Years { get; set; }

        [Required]
        [DisplayName("Prix (en €)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0.")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        public int MaxSubscriptions { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }

        public bool ForSale { get; set; }
        public byte[] ImgSmallBoxContract { get; set; }
        public byte[] ImgBigBoxContract { get; set; }

    }


}
