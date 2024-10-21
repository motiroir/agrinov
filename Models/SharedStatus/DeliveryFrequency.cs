using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models.ProductionModel

{
    public enum DeliveryFrequency
    {
        [Display(Name = "Hebdomadaire")]
        WEEKLY,

        [Display(Name = "Bihebdomadaire")]
        BIWEEKLY,

        [Display(Name = "Mensuelle")]
        MONTHLY

    }
}
