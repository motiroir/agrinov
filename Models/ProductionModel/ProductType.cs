using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models.ProductionModel
{
    public enum ProductType
    {
        [Display(Name = "Sélectionnez un type de produit")]
        None = 0,

        [Display(Name = "Légumes")]
        VEGETABLES = 1,

        [Display(Name = "Fruits")]
        FRUITS = 2,

        [Display(Name = "Oeufs")]
        EGGS = 3,

        [Display(Name = "Laitages")]
        DAYRIS = 4,

        [Display(Name = "Poissons")]
        FISH = 5,

        [Display(Name = "Viandes")]
        MEAT = 6
    }
}
