using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models.ProductionModel
{
    public enum ProductType
    {
        [Display(Name = "Légumes")]
        VEGETABLES,

        [Display(Name = "Fruits")]
        FRUITS,

        [Display(Name = "Oeufs et Produits Laitiers")]
        EGGS_AND_DAIRY,

        [Display(Name = "Poissons")]
        FISH,

        [Display(Name = "Viandes")]
        MEAT
    }
}
