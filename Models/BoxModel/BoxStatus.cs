using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models
{
    public enum BoxStatus
    {
        [Display(Name = "Pas récupéré")]
        NOT_COLLECTED,

        [Display(Name = "Récupéré")]
        COLLECTED
    }
}
