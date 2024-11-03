using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models
{
    public enum PaymentType
    {
        [Display(Name = "NA")]
        NA,
        [Display(Name = "CB")]
        CARD,
        [Display(Name = "Espèces")]
        CASH,
        [Display(Name = "Chèque")]
        CHECK
    }
}