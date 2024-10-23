using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models
{
    public enum ValidationStatus
    {
        [Display(Name = "En attente")]
        WAITING,
        [Display(Name = "Approuvé")]
        APPROVED,
        [Display(Name = "Rejeté")]
        REFUSED
    }
}
