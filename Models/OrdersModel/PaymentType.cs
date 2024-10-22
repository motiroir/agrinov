using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models
{
    public enum PaymentType
    {
        [Display(Name = "CB")]
        CARD,
        [Display(Name = "Escpèces")]

        CASH,
        [Display(Name = "Chèque")]
        CHECK
    }
}