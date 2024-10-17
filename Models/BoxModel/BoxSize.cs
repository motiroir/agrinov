using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models.BoxModel
{
    public enum BoxSize
    {
        [Display(Name = "Grand")]
        L,

        [Display(Name = "Moyen")]
        M,

        [Display(Name = "Petit")]
        S
    }
    
}
