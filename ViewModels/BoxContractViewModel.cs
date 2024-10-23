using AgriNov.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgriNov.ViewModels
{
    public class BoxContractViewModel
    {
        public BoxContract BoxContract { get; set; }
        public Production Production { get; set; }
        public IEnumerable<SelectListItem> YearOptions { get; set; }
        public IEnumerable<SelectListItem> ProductOptions { get; set; }
        public IEnumerable<SelectListItem> SeasonOptions { get; set; }


        [Required]
        [DisplayName("Type de produit")]
        [Range(1, int.MaxValue, ErrorMessage = "Veuillez sélectionner un produit.")]
        public ProductType ProductType { get; set; }

        [Required]
        [DisplayName("Saison")]
        [Range(1, int.MaxValue, ErrorMessage = "Veuillez sélectionner une saison.")]
        public Seasons Seasons { get; set; }

        [Required]
        [DisplayName("Année")]
        [Range(1, int.MaxValue, ErrorMessage = "Veuillez sélectionner une année.")]
        public Years Years { get; set; }


        [DisplayName("Stock total (kg/l/pièce)")]
        public int GlobalStock { get; internal set; }
        [DisplayName("Quantité par panier (kg/l/pièce)")]
        public int QuantityPerBox { get; internal set; }
    }
}
